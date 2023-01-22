using Dapper;
using WaitForIt.Application.Contracts;
using WaitForIt.Application.Features.Commands.Create;
using WaitForIt.Application.Features.Commands.Remove;
using WaitForIt.Application.Features.Commands.Update;
using WaitForIt.Domain;

namespace WaitForIt.Persistence.Repository;

public class EntityRepository : DBRepository, IAsyncEntityRepository
{
    public async Task<Guid> Add(Entity entity)
    {
        string sql = "INSERT INTO Entities (Name, Description, Quantity) " +
            "VALUES (@Name, @Description, @Quantity)";

        var param =
            new
            {
                Name = entity.Name,
                Description = entity.Description,
                Quantity = entity.Quantity
            };

        entity.Id = (await db.QueryAsync<Guid>(sql, param))
            .Single();

        return entity.Id;
    }


    public async Task<dynamic> ApplyEventAsync(Event @event)
    {
        switch (@event.EventName)
        {
            case nameof(CreateEntity):
                {
                    var cEntEvnt = (CreateEntity)@event;
                    var entity = MappToEntityFromCreateEntityEvent(cEntEvnt);
                    return await Add(entity);
                }
            case nameof(UpdateEntity):
                {
                    var uEntEvnt = (UpdateEntity)@event;
                    var entity = MappToEntityFromUpdateEntity(uEntEvnt);
                    return await Update(entity);
                }
            case nameof(RemoveEntity):
                {
                    var rEntEvnt = (RemoveEntity)@event;
                    return await Remove(rEntEvnt.Id);
                }
            default:
                throw new InvalidOperationException($"operation by name {@event.EventName} coud not implement as event");
        }
    }

    public async Task<IEnumerable<Entity>> GetAll()
    {
        string sql = "SELECT * FROM Entities";
        var entity = await db.QueryAsync<Entity>(sql);

        return entity;
    }

    public async Task<Entity> GetById(Guid id)
    {
        string sql = "SELECT * FROM Entities WHERE Id = @Id";
        var entity = await db.QueryFirstOrDefaultAsync<Entity>(sql, new { Id = id });

        return entity;
    }

    public async Task<(bool IsDelete, string Msg)> Remove(Guid id)
    {
        string sql = "DELETE FROM Entities WHERE Id = @Id";

        (bool IsDelete, string Msg) result = new();

        try
        {
            await db.ExecuteAsync(sql, new { Id = id });

            result.IsDelete = true;
            result.Msg = $"Entity by id : {id} deleted ...!";

        }
        catch (Exception e)
        {
            result.IsDelete = false;
            result.Msg = $"Error : {e.Message}";
        }

        return result;
    }

    public async Task<(bool IsUpdate, string Msg)> Update(Entity entity)
    {
        string sql = "UPDATE Entities SET Name = @Name, Description = @Description, Quantity = @Quantity WHERE Id = @Id";

        (bool IsUpdate, string Msg) result = new();

        try
        {
            var param = new
            {
                Name = entity.Name,
                Description = entity.Description,
                Quantity = entity.Quantity,
                Id = entity.Id,
            };
            await db.ExecuteAsync(sql, param);

            result.IsUpdate = true;
            result.Msg = $"Entity by id : {entity.Id} updated ...!";

        }
        catch (Exception e)
        {
            result.IsUpdate = false;
            result.Msg = $"Error : {e.Message}";
        }

        return result;


    }

    #region Helpers

    Func<CreateEntity,Entity> MappToEntityFromCreateEntityEvent => (cEnt) 
        => new Entity 
        {
            Id = new Guid(cEnt.EntityId) ,
            Description= cEnt.Description,
            Quantity= cEnt.Quantity ,
            Name = cEnt.Name ,
            events = new() 
        };
    Func<UpdateEntity, Entity> MappToEntityFromUpdateEntity => (uEnt)
    => new Entity
    {
        Id = new Guid(uEnt.EntityId),
        Description = uEnt.Description,
        Quantity = uEnt.Quantity,
        Name = uEnt.Name
    };

    #endregion

}
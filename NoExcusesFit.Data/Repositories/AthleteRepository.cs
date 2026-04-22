using Dapper;
using NoExcusesFit.Data.Persistence;
using NoExcusesFit.Domain.Entities;
using NoExcusesFit.Domain.Interfaces.Repositories;
using System.Data;

namespace NoExcusesFit.Data.Repositories;

public class AthleteRepository : IAthleteRepository
{
    private readonly DapperContext _dapperContext;

    public AthleteRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task AddAsync(Athlete athlete)
    {
        const string sql = @"
            INSERT INTO Athlete (Id, UserAccountId, CoachId)
            VALUES (@Id, @UserAccountId, @CoachId);";

        using var connection = _dapperContext.CreateConnection();

        await connection.ExecuteAsync(sql, athlete);
    }

    public async Task AddAsync(Athlete athlete, IDbConnection connection, IDbTransaction transaction)
    {
        const string sql = @"
            INSERT INTO Athlete (Id, UserAccountId, CoachId)
            VALUES (@Id, @UserAccountId, @CoachId);";

        await connection.ExecuteAsync(sql, athlete, transaction);
    }

    public async Task<Athlete?> GetByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT * 
            FROM Athlete
            WHERE Id = @Id";

        using var connection = _dapperContext.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Athlete>(sql, new { Id = id });
    }

    public async Task<List<Athlete>> GetAllAsync(int skip, int take)
    {
        const string sql = @"
            SELECT * FROM Athlete
            ORDER BY Id
            OFFSET @Skip ROWS 
            FETCH NEXT @Take ROWS ONLY";

        using var connection = _dapperContext.CreateConnection();

        var result = await connection.QueryAsync<Athlete>(sql, new { Skip = skip, Take = take });
        return result.ToList();
    }

    public async Task UpdateAsync(Athlete athlete)
    {
        const string sql = @"
            UPDATE Athlete
            SET CoachId = @CoachId
            WHERE Id = @Id";

        using var connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(sql, athlete);
    }


    public async Task DeleteAsync(Guid id)
    {
       const string sql = @"
            DELETE FROM Athlete
            WHERE Id = @Id";

        using var connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task DeleteAsync(Guid id, IDbConnection connection, IDbTransaction transaction)
    {
        const string sql = @"
            DELETE FROM Athlete
            WHERE Id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction);
    }
}
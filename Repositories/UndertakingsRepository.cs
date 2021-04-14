using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using afternoon.Models;
using Dapper;

namespace afternoon.Repositories
{
    public class UndertakingsRepository
    {
        private readonly IDbConnection _db;

        public UndertakingsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Undertaking> Get()
        {
            string sql = @"
      SELECT
      u.*,
      pr.*
      FROM undertakings u
      JOIN profiles pr ON u.creatorId = pr.id";
            return _db.Query<Undertaking, Profile, Undertaking>(sql, (undertaking, profile) =>
            {
                undertaking.Creator = profile;
                return undertaking;
            }, splitOn: "id");
        }

        internal Undertaking GetById(int id)
        {
            string sql = @"
      SELECT 
      part.*,
      pr.*
      FROM undertakings part
      JOIN profiles pr ON part.creatorId = pr.id
      WHERE part.id = @id;";
            return _db.Query<Undertaking, Profile, Undertaking>(sql, (undertaking, profile) =>
            {
                undertaking.Creator = profile;
                return undertaking;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Undertaking Create(Undertaking newundertaking)
        {
            string sql = @"
      INSERT INTO undertakings
      (creatorId, name, public)
      VALUES
      (@CreatorId, @Name, @Public);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newundertaking);
            newundertaking.Id = id;
            return newundertaking;
        }

        internal Undertaking Edit(Undertaking updated)
        {
            string sql = @"
        UPDATE undertakings
        SET
            name = @Name
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM undertakings WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}
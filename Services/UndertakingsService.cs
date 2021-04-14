using System;
using System.Collections.Generic;
using afternoon.Models;
using afternoon.Repositories;

namespace afternoon.Services
{
    public class UndertakingsService
    {
        private readonly UndertakingsRepository _urepo;
        public UndertakingsService(UndertakingsRepository urepo)
        {
            _urepo = urepo;
        }


        internal IEnumerable<Undertaking> Get()
        {
            return _urepo.Get();
        }

        internal Undertaking GetById(int id)
        {
            Undertaking data = _urepo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid id");
            }
            return data;
        }

        internal Undertaking Create(Undertaking newUndertaking)
        {
            return _urepo.Create(newUndertaking);
        }

        internal Undertaking Edit(Undertaking updated)
        {
            Undertaking original = GetById(updated.Id);
            if (updated.creatorId != original.creatorId)
            {
                throw new Exception("You cannot edit this.");
            }
            return _urepo.Edit(updated);
        }

        internal Undertaking Delete(int id, string userId)
        {
            Undertaking original = GetById(id);
            if (userId != original.creatorId)
            {
                throw new Exception("You cannot delete this.");
            }
            _urepo.Delete(id);
            return original;
        }
    }
}
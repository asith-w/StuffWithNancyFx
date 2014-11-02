﻿using System;
using System.Collections.Generic;
using System.Linq;
using CrudsWithNancyFx.Helper;
using NHibernate.Linq;

namespace CrudsWithNancyFx.Models.Persistence
{
    public class ServerDataRepository : IServerDataRepository
    {
        public ServerDataRepository()
        {

        }

        public ServerData Get(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Get<ServerData>(id);
        }

        public IEnumerable<ServerData> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Query<ServerData>().ToList();
        }

        public ServerData Add(ServerData serverData)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(serverData);
                    transaction.Commit();
                }
                return serverData;
            }
        }

        public void Delete(int id)
        {
            var serverData = Get(id);

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(serverData);
                    transaction.Commit();
                }
            }

        }

        public bool Update(ServerData serverData)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(serverData);
                    try
                    {
                        transaction.Commit();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                return true;
            }
        }
    }
}
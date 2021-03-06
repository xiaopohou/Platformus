﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.SqlServer
{
  public class UserRepository : RepositoryBase<User>, IUserRepository
  {
    public User WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredUsers(this.dbSet, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(User user)
    {
      this.dbSet.Add(user);
    }

    public void Edit(User user)
    {
      this.storageContext.Entry(user).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(User user)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM UserRoles WHERE UserId = {0};
          DELETE FROM Credentials WHERE UserId = {0};
        ",
        user.Id
      );

      this.dbSet.Remove(user);
    }

    public int Count(string filter)
    {
      return this.GetFilteredUsers(this.dbSet, filter).Count();
    }

    private IQueryable<User> GetFilteredUsers(IQueryable<User> users, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return users;

      return users.Where(u => u.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}
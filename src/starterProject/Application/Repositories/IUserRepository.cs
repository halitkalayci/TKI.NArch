﻿using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;
public interface IUserRepository : IAsyncRepository<User,int>, IRepository<User,int>
{
}

﻿using GeoSpotter.API.Data;
using GeoSpotter.API.Entities;
using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoSpotter.API.Persistence.Repositories;

public class ApiMessageRepository : IApiMessageRepository
{
    private readonly ApplicationDbContextFactory _applicationDbContextFactory;

    public ApiMessageRepository(ApplicationDbContextFactory applicationDbContextFactory)
    {
        _applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task AddApiMessageAsync(ApiMessage apiMessage)
    {
        await using var dbContext = _applicationDbContextFactory.CreateDbContext();

        await dbContext.ApiMessages.AddAsync(apiMessage);

        await dbContext.SaveChangesAsync();
    }

    public async Task<IList<ApiMessage>> GetApiMessagesAsync()
    {
        await using var dbContext = _applicationDbContextFactory.CreateDbContext();

        return await dbContext
            .ApiMessages
            .ToListAsync();
    }
}
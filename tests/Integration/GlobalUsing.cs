global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using Core.Entities;
global using Core.DTOs.Invitation;
global using Core.DTOs;
global using Core.DTOs.Event;
global using Core.DTOs.Participant;
global using Core.Utilities.Constants;
global using Core.Utilities.Results;
global using Core.Interfaces.Repositories;
global using Core.Interfaces.Services;

global using Infrastructure.Data;
global using Infrastructure.ExtensionMethods;

global using Integration.Base;
global using Integration.Fixtures;
global using Integration.Harness;

global using Xunit;
global using FluentAssertions;
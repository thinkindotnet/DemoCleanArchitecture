﻿using AutoMapper;

using MyApp.Application.ManageCategoriesFeature.DTOs;
using MyApp.Domain.Entities;
using MyApp.xUnitTests.Common;

namespace MyApp.xUnitTests;


// Integration Test between the Domain Models and Application Models

public class MappingTests
    : IClassFixture<MappingFixture>
{
    private readonly IMapper _mapper;

    public MappingTests(MappingFixture fixture)
    {
        _mapper = fixture.Mapper;
    }


    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        // Prepare

        // Act

        // Assert
        _mapper
            .ConfigurationProvider
            .AssertConfigurationIsValid();
    }


    [Theory]
    [InlineData(typeof(Category), typeof(CategoryDto))]
    // [InlineData(typeof(Product), typeof(ProductDto))]
    public void ShoudSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        // Prepare
        var instance = Activator.CreateInstance(source, destination);

        // Act

        // Assert
        _mapper.Map(instance, source, destination);
    }

}

﻿namespace ApiGateway.Core.Filters
{
    public interface IFilter
    {
        FilterType FilterType { get; }
        int FilterOrder { get; }
        void Execute();
    }

    public enum FilterType
    {
        Pre,
        Post,
        Route,
        Error
    }
}

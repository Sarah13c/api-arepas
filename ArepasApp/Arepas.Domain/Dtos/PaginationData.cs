﻿namespace Arepas.Domain.Dtos;

public class PaginationData
{
    public PaginationData(int totalCount, int currentPage, int limit)
    {
        First = 1;
        TotalCount = totalCount;
        Limit = limit;
        Last = (int)Math.Ceiling(totalCount / (double)limit);
        Page = currentPage < Last ? currentPage : Last;
        Next = currentPage < Last ? currentPage + 1 : Last;
        Previous = currentPage > First ? Page - 1 : First;
    }

    public int First { get; init; }
    public int Last { get; init; }
    public int Limit { get; init; }
    public int Next { get; init; }
    public int Page { get; init; }
    public int Previous { get; init; }
    public int TotalCount { get; init; }
}
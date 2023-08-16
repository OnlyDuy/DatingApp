export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
    // T đại diện cho 1 mảng members
    result: T;
    pagination: Pagination;
}
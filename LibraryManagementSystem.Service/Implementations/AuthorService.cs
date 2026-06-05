using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;

namespace LibraryManagementSystem.Service.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorStore _authorStore;

    public AuthorService(
        IAuthorStore authorStore)
    {
        _authorStore = authorStore;
    }

    public async Task<AuthorResponseDto> CreateAuthorAsync(
        CreateAuthorRequestDto request)
    {
        try
        {
            var author = new Author
            {
                AuthorName = request.AuthorName,

                IsActive = true,

                CreatedOn = DateTime.UtcNow
            };

            var createdAuthor =
                await _authorStore.CreateAuthorAsync(
                    author);

            return new AuthorResponseDto
            {
                UniqueId = createdAuthor.UniqueId,

                AuthorName = createdAuthor.AuthorName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(CreateAuthorAsync)}: {ex.Message}");
        }
    }

    public async Task<List<AuthorResponseDto>>
        GetAllAuthorsAsync()
    {
        try
        {
            var authors =
            await _authorStore.GetAllAuthorsAsync();

            return authors.Select(author =>
                new AuthorResponseDto
                {
                    UniqueId = author.UniqueId,

                    AuthorName = author.AuthorName
                }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllAuthorsAsync)}: {ex.Message}");
        }
    }

    public async Task<AuthorResponseDto> GetAuthorByUniqueIdAsync(
            string uniqueId)
    {
        try
        {
            var author =
            await _authorStore
                .GetAuthorByUniqueIdAsync(
                    uniqueId);

            if (author == null)
            {
                throw new Exception(
                    "Author not found");
            }

            return new AuthorResponseDto
            {
                UniqueId = author.UniqueId,

                AuthorName = author.AuthorName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAuthorByUniqueIdAsync)}: {ex.Message}");
        }
    }


    public async Task<PagedResponseDto<AuthorResponseDto>> GetAuthorsPagedAsync(
        PaginationRequestDto request)
    {
        try
        {
            var result =
                await _authorStore.GetAuthorsPagedAsync(
                    request.PageNumber,
                    request.PageSize);

            return new PagedResponseDto<AuthorResponseDto>
            {
                PageNumber = request.PageNumber,

                PageSize = request.PageSize,

                TotalRecords = result.TotalRecords,

                TotalPages =
                    (int)Math.Ceiling(
                        (double)result.TotalRecords /
                        request.PageSize),

                Data = result.Authors
                    .Select(author =>
                        new AuthorResponseDto
                        {
                            UniqueId = author.UniqueId,

                            AuthorName = author.AuthorName
                        })
                    .ToList()
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAuthorsPagedAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateAuthorAsync(
        string uniqueId,
        UpdateAuthorRequestDto request)
    {
        try
        {
            var author =
                await _authorStore
                    .GetAuthorByUniqueIdAsync(
                        uniqueId);

            if (author == null)
            {
                throw new Exception(
                    "Author not found");
            }

            author.AuthorName =
                request.AuthorName;

            author.UpdatedOn =
                DateTime.UtcNow;

            await _authorStore
                .UpdateAuthorAsync(author);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(UpdateAuthorAsync)}: {ex.Message}");
        }
    }

    public async Task DeleteAuthorAsync(
        string uniqueId)
    {
        try
        {
            var author =
                await _authorStore
                    .GetAuthorByUniqueIdAsync(
                        uniqueId);

            if (author == null)
            {
                throw new Exception(
                    "Author not found");
            }

            await _authorStore
                .SoftDeleteAuthorAsync(author);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(DeleteAuthorAsync)}: {ex.Message}");
        }
    }
}
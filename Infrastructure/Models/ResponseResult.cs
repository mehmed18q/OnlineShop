using Core.Models;
using System.Net;

namespace Infrastructure.Models
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess => new List<HttpStatusCode> { HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.Accepted }.Contains(StatusCode);
    }

    public class ListResponse<T> : Response<T>
    {
        public Pagination? Pagination { get; set; }
    }

    public static class Response
    {
        public static Response<T> Result<T>(T? data, string? message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Response<T>()
            {
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ListResponse<List<T>> ListResult<T>(List<T>? data, string? message, Pagination? pagination, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ListResponse<List<T>>()
            {
                Data = data,
                Message = message,
                StatusCode = statusCode,
                Pagination = pagination
            };
        }
    }
}


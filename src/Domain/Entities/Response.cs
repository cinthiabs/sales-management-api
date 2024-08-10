using Domain.Enums;
using System.ComponentModel;

namespace Domain.Entities
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public Status? Code { get; set; }
        public string? Message { get; set; }
        public bool IsFailure => !IsSuccess;
        public T[]? Data { get; set; }

        public static Response<T> Success(T data, Status? status = null)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Code = status,
                Message = status.HasValue ? GetEnumDescription(status.Value) : null,
                Data = [data]
            };
        }

        public static Response<T> Success(T[] data, Status? status = null)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Code = status,
                Message = status.HasValue ? GetEnumDescription(status.Value) : null,
                Data = data
            };
        }

        public static Response<T> Failure(Status status)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Code = status,
                Message = GetEnumDescription(status)
            };
        }

        private static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            DescriptionAttribute? attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description ?? value.ToString();
        }
    }
}

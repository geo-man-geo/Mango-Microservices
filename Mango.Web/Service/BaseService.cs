﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Models.Utilities.SD;


namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        { 
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
        try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //token
                if (withBearer)
                {
                    var token  = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }
                if (requestDto.Url == null)
                {
                    throw new ArgumentNullException(nameof(requestDto.Url), "URL cannot be null");
                }
                message.RequestUri = new Uri(requestDto.Url);

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            switch(requestDto.ApiType)
            {
                case ApiType.POST:  
                    message.Method = HttpMethod.Post;   
                    break;
                case ApiType.PUT: 
                    message.Method = HttpMethod.Put;    
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);

                if (apiResponse != null)
                {
                    switch (apiResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return new() { IsSuccess = false, Message = "Not Found" };

                        case HttpStatusCode.Unauthorized:
                            return new() { IsSuccess = false, Message = "Unauthorized" };

                        case HttpStatusCode.BadRequest:
                            return new() { IsSuccess = false, Message = "Bad Request" };

                        case HttpStatusCode.Forbidden:
                            return new() { IsSuccess = false, Message = "Forbidden" };

                        case HttpStatusCode.InternalServerError:
                            return new() { IsSuccess = false, Message = "Internal Server Error" };

                        case HttpStatusCode.BadGateway:
                            return new() { IsSuccess = false, Message = "Bad Gateway" };

                        case HttpStatusCode.ServiceUnavailable:
                            return new() { IsSuccess = false, Message = "Service Unavailable" };

                        case HttpStatusCode.GatewayTimeout:
                            return new() { IsSuccess = false, Message = "Gateway Timeout" };

                        default:
                            var apiContent = await apiResponse.Content.ReadAsStringAsync();
                            var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                            return apiResponseDto;

                    }
                }
                else
                {
                    return new ResponseDto() { IsSuccess = false, Message = "API Response is null" };
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto()
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
            return dto;
            }
        }


    }
}

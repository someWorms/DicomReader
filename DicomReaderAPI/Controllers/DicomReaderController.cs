using DicomReaderAPI.Services;
using DicomReaderAPI.Utils;
using EventBus.Messaging.Events;
using FellowOakDicom;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DicomReaderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DicomReaderController
    {
        private readonly HttpResponseMessage _response;
        private readonly IDicomReaderService _service;
        private readonly IBus _endp;
        private readonly ILogger _logger;

        public DicomReaderController(IDicomReaderService service, IBus endp, ILogger<DicomReaderController> logger)
        {
            _response = new HttpResponseMessage();
            _service = service;
            _logger = logger;
            _endp = endp;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<HttpResponseMessage> ProcessData(IFormFile file)
        {
            if (!DicomValidate.IsDicomFile(file.OpenReadStream()))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Content = new StringContent("File must be .dcm format");
                return _response;
            }
            var result = await _service.ReadFile(file);

            _logger.LogInformation(result.ToString());

            if (result == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Content = new StringContent("File processing error");
                return _response;
            }    

            var eventMessage = new DataEvent()
            {
                StuduIUID = result.StudyIUID
            };

            await _endp.Publish<DataEvent>(eventMessage);

            _response.StatusCode = HttpStatusCode.Accepted;
            return _response;
        }
    }
}

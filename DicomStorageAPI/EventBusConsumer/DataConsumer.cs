using DicomStorageAPI.Models;
using DicomStorageAPI.Services.Interfaces;
using EventBus.Messaging.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DicomStorageAPI.EventBusConsumer
{
    public class DataConsumer : IConsumer<DataEvent>
    {

        private readonly IDataService _dataService;
        public DataConsumer(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task Consume(ConsumeContext<DataEvent> context)
        {
            var data = context.Message;
            var result = new DicomData
            {
                StudyIUID = data.StuduIUID
            };

            await _dataService.SaveData(result);
        }
    }
}

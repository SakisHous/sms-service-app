using Microsoft.EntityFrameworkCore;
using SmsApp.Data;
using SmsApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsApp.Tests.Repositories
{
    public class SmsRepositoryTests
    {
        [Fact]
        public async Task AddMessageAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ShortMessagesDbContext>()
                .UseInMemoryDatabase(databaseName: "MessagesTestDB")
                .Options;

            using (var dbContext = new ShortMessagesDbContext(options))
            {
                var repository = new SmsRepository(dbContext);

                // Create a record to insert
                var record = new ShortMessage
                {
                    MessagesId = 1,
                    MessageBody = "Test ShortMessage",
                    SenderCountryCode = "+30",
                    Sender = "6951234567",
                    RecipientCountryCode = "+30",
                    Recipient = "6901234567",
                    Vendor = "smsGrVendor"
                };

                await repository.AddSmsAsync(record);
                await dbContext.SaveChangesAsync();

                // Assert
                var insertedRecord = await dbContext.ShortMessages.FindAsync(record.MessagesId);
                
                // Check if the record exists in the database
                Assert.NotNull(insertedRecord);

                // Check if the inserted record has the correct properties
                Assert.Equal(record.MessageBody, insertedRecord.MessageBody);
            }
        }
    }
}

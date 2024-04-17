# Sms Service 

This Sms Service redirects a Sms to one of three vendors which support the sending of the message to the customers.

We create a database, e.g. with the name SmsMessagesDB and collation "Greek_100_CI_AI" in order to store both greek and english SMS messages.
CI means Case Insensitive and AI means Accent Insensitive. In addition, we create a simple table for SMS messages where all three vendors store their messages. We run the SQL command for the creation of the table,

```
CREATE TABLE ShortMessages (
	messages_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	message_body NVARCHAR(512),
	sender_country_code VARCHAR(5),
	sender NVARCHAR(50),
	recipient_country_code VARCHAR(5),
	recipient NVARCHAR(50),
	vendor NVARCHAR(50)
);
```

In addition, a user with db owner role was created. With this user, we connect to the database from the application.

Using DbContext for Scaffolding the entities and the application's Db context with the command

```
DbContext-Scaffold 'Server=localhost\\sqlexpress;Database=<DatabaseName>;User=<User>;Password=<Password>;MultipleActiveResultSets=True;TrustServerCertificate=True'
```


The main application was built with ASP.NET Core Web API. In addition an Event Driven Architecture approach was followed for the main controller.

For that purpose, RabbitMQ was used where events were sent in the consumers. Consumers are located in certain endpoints and they are basically services that process the message.

A RabbitMQ Server was set up using Docker. The command for creating a container is

```
docker run -d --hostname rabbitmq --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

If there is no image an image wil be downloaded from Docker Hub.

In addition, the Task asynchronous programming model (TAP) was used and Repository Design Pattern is used main database operations.
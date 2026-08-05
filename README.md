Chạy Seq miễn phí bằng Docker
1. Mở PowerShell và chạy lệnh
docker run --name seq ` -e ACCEPT_EULA=Y ` -p 5341:80 ` -d datalust/seq

2. RabbitMQ
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management

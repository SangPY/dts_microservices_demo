Chạy Seq miễn phí bằng Docker
1. Mở PowerShell và chạy lệnh
docker run --name seq `
-e ACCEPT_EULA=Y `
-p 5341:80 `
-d datalust/seq

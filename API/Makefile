
.PHONY: dev
dev: migrate
	dotnet watch --no-hot-reload

.PHONY: migrate
migrate:
	rm ../Infrastructure/Data/Migrations/*.cs
	dotnet ef migrations add InitialCreate \
		-p ../Infrastructure \
		-o ../Infrastructure/Data/Migrations
	dotnet ef database update


.PHONY: dev
dev: reset-db
	dotnet watch --no-hot-reload

.PHONY: reset-db
reset-db:
	rm -f Continuous.db && \
		rm ../Infrastructure/Data/Migrations/*.cs && \
		dotnet ef migrations add InitialCreate \
			-p ../Infrastructure \
			-o ../Infrastructure/Data/Migrations && \
		dotnet ef database update

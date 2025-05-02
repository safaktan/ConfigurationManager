CREATE TABLE IF NOT EXISTS "ConfigurationParameters" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Value" VARCHAR(500) NOT NULL,
    "Type" VARCHAR(50) NOT NULL,
    "IsActive" BOOLEAN NOT NULL,
    "ApplicationName" VARCHAR(100) NOT NULL,
    "UpdatedAt" TIMESTAMP DEFAULT NOW()
);
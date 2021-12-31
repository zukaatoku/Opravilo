-- отвратительно, перепридумать

INSERT INTO "Users" ("LOGIN", "PASSWORD_HASH", "CREATED_DATE", "CHANGED_DATE", "DISPLAY_NAME")
SELECT 'root', 'gjLP+At5b9LUKBFTQAIEYtX5OANpQPjn3KlUBKdlS/k=', current_timestamp, current_timestamp,'root'
    WHERE NOT EXISTS (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root');

INSERT INTO "Projects" ("NAME", "DESCRIPTION", "CREATOR_ID", "CREATED_DATE", "CHANGED_DATE")
SELECT 'Sandbox', 'Empty project for tests', (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root'), current_timestamp, current_timestamp
    WHERE NOT EXISTS (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root'));

INSERT INTO "States" ("NAME", "PROJECT_ID", "CREATED_DATE", "CHANGED_DATE")
SELECT 'Unknown', (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')), current_timestamp, current_timestamp
    WHERE NOT EXISTS (SELECT "ID" FROM "States" WHERE "NAME" LIKE 'Unknown' AND "PROJECT_ID" = (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')));

INSERT INTO "States" ("NAME", "PROJECT_ID", "CREATED_DATE", "CHANGED_DATE")
SELECT 'Planned', (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')), current_timestamp, current_timestamp
    WHERE NOT EXISTS (SELECT "ID" FROM "States" WHERE "NAME" LIKE 'Planned' AND "PROJECT_ID" = (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')));

INSERT INTO "States" ("NAME", "PROJECT_ID", "CREATED_DATE", "CHANGED_DATE")
SELECT 'In work', (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')), current_timestamp, current_timestamp
    WHERE NOT EXISTS (SELECT "ID" FROM "States" WHERE "NAME" LIKE 'In work' AND "PROJECT_ID" = (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')));

INSERT INTO "States" ("NAME", "PROJECT_ID", "CREATED_DATE", "CHANGED_DATE")
SELECT 'Done', (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')), current_timestamp, current_timestamp
    WHERE NOT EXISTS (SELECT "ID" FROM "States" WHERE "NAME" LIKE 'Done' AND "PROJECT_ID" = (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root')));

INSERT INTO "Cards" ("NAME", "DESCRIPTION", "STATE_ID", "CREATED_DATE", "CHANGED_DATE")
SELECT 'Test card', 'Lorem ipsum', (SELECT "ID" FROM "States" WHERE "NAME" LIKE 'Unknown' AND "PROJECT_ID" = (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root'))), current_timestamp, current_timestamp
    WHERE NOT EXISTS (SELECT "ID" FROM "Cards" WHERE "NAME" LIKE 'Test card' AND "STATE_ID" = (SELECT "ID" FROM "States" WHERE "NAME" LIKE 'Unknown' AND "PROJECT_ID" = (SELECT "ID" FROM "Projects" WHERE "NAME" LIKE 'Sandbox' AND "CREATOR_ID" = (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root'))));
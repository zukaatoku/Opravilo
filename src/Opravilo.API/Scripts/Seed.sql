-- отвратительно, перепридумать

INSERT INTO "Users" ("LOGIN", "PASSWORD_HASH", "CREATED_DATE", "CHANGED_DATE", "DISPLAY_NAME")
SELECT 'root', 'gjLP+At5b9LUKBFTQAIEYtX5OANpQPjn3KlUBKdlS/k=', current_timestamp, current_timestamp,'root'
    WHERE NOT EXISTS (SELECT "ID" FROM "Users" WHERE "LOGIN" LIKE 'root');
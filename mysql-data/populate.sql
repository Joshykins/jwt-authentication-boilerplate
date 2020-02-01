INSERT INTO
  dbo.accounts (account_name, date_created)
VALUES
  ('test123', GETDATE());

INSERT INTO
  dbo.users (email, password, date_created, account_id)
VALUES
  (
    'joshcoffee@joshuascoffee.biz',
    '!Jroe123',
    GETDATE(),
    (
      SELECT
        id
      from
        dbo.accounts
      WHERE
        account_name = 'test123'
    )
  );


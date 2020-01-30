INSERT INTO
  `accounts` (`account_name`, `date_created`, `owner_id`)
VALUES
  ( "Josh's Coffee Shop", CURDATE(), null);

INSERT INTO
  `users` (
    `email`,
    `password`,
    `date_created`,
    `account_id`
  )
VALUES
  (
    "joshcoffee@joshuascoffee.biz",
    "!joshcoffeshop123",
    CURDATE(),
    (SELECT
      `id`
    FROM
      `accounts`
    WHERE
      `account_name` = "Josh's Coffee Shop")
  );

  SELECT * FROM users
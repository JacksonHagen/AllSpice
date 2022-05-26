CREATE TABLE IF NOT EXISTS accounts(
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    picture VARCHAR(255) DEFAULT "https://thiscatdoesnotexist.com",
    title VARCHAR(255) DEFAULT "A Tasty Recipe",
    subtitle TEXT,
    category VARCHAR(255),
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS ingredients(
    name VARCHAR(255) NOT NULL,
    quantity VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN Key (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS steps(
    postition INT NOT NULL,
    body TEXT NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS favorites(
    accountId VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;
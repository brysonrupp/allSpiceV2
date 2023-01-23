CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(255) NOT NULL,
        instructions TEXT,
        img VARCHAR(255),
        category VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    favorites(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        recipeId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        Foreign Key (recipeId) REFERENCES recipes (id) ON DELETE CASCADE,
        Foreign Key (accountId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8;

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        'magic brownies',
        'add a little potion and spin in a circular motion',
        'https://www.spendwithpennies.com/wp-content/uploads/2022/03/Homemade-Brownies.jpeg',
        'baking',
        '639cc3a716fd91062823ec82'
    )
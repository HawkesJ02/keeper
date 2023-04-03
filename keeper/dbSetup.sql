CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS profiles(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  accountId VARCHAR(255) NOT NULL,
  Name VARCHAR(255) NOT NULL,
  Picture VARCHAR (255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (Name) REFERENCES accounts(name) ON DELETE CASCADE,
  FOREIGN KEY (Picture) REFERENCES accounts(picture) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(50) COMMENT 'Name of Keep' NOT NULL,
  Description VARCHAR(500) COMMENT 'Description of Keep' NOT NULL,
  Img VARCHAR(500) COMMENT "Keep's Image URL",
  Views INT NOT NULL DEFAULT 0,
  Kept INT NOT NULL DEFAULT 0, 
  CreatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (CreatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CreatorId VARCHAR(255) NOT NULL,
  Name VARCHAR(50) COMMENT 'Name of Vault' NOT NULL,
  Description VARCHAR(500) COMMENT 'Description of Vault' NOT NULL,
  IsPrivate BOOLEAN NOT NULL COMMENT 'Determines if shown to all users or the active logged in user' DEFAULT  false,

  FOREIGN KEY (CreatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CreatorId VARCHAR(255) NOT NULL,
  VaultId INT NOT NULL,
  KeepId INT NOT NULL,

  FOREIGN KEY (CreatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (VaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (KeepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE vaults;
-- USE TO DEMOLISH FOREVER THE VAULTS TABLE IF NEEDED >:) OTHERWISE, do NOT HIT THIS BUTTON

DROP TABLE keeps;
-- USE TO NUKE THE DARN KEEPS TABLE IF NEEDED :) OTHERWISE, do NOT HIT THIS BUTTON

DROP TABLE vaultkeeps;
-- I'm raging bro

SELECT *
FROM keeps
JOIN accounts creator ON keeps.CreatorId = creator.id GROUP BY keeps.id;

SELECT *
FROM keeps
JOIN accounts creator ON keeps.CreatorId = creator.id
WHERE keeps.id = 1;

SELECT *
FROM vaults
JOIN accounts creator ON vaults.CreatorId = creator.id 
WHERE vaults.Id = 2;

SELECT *
FROM vaultkeeps
JOIN accounts creator ON vaultkeeps.CreatorId = creator.id
JOIN vaults ON vaultkeeps.VaultId = vaults.id
JOIN keeps ON vaultkeeps.KeepId = keeps.id
WHERE vaultkeeps.VaultId = 18;

SELECT * 
FROM accounts
WHERE ;

UPDATE keeps
SET
Name = 'wuh',
Description = 'wuvly',
Img = 'https://plus.unsplash.com/premium_photo-1667030474693-6d0632f97029?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80',
Views = 0
WHERE keeps.id = 6;


DELETE FROM keeps
WHERE id = 6;

INSERT INTO vaultkeeps
(`CreatorId`, `VaultId`, `KeepId`)
VALUES
('64233077a1188ee878fee3ca', '1', '1');


INSERT INTO keeps
(`Name`, `Description`, `Img`, `CreatorId` )
VALUES
('Box Dweller', 'Why he in the boxy dough? What terrible unspeakable horrors has thou committed to be entrenched into such a fate?','https://images.unsplash.com/photo-1498579687545-d5a4fffb0a9e?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80','64233077a1188ee878fee3ca' );

INSERT INTO keeps
(`Name`, `Description`, `Img`, `CreatorId` )
VALUES
('KITTEN', 'His soul is lit afire, let us hope you are not the target of his gaze lest thou feels the scrouge of the one they call kitten','https://images.unsplash.com/photo-1535467487981-c86a98db6b9c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80','64233077a1188ee878fee3ca' )



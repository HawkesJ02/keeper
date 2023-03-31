CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(50) COMMENT 'Name of Keep' NOT NULL,
  Description VARCHAR(500) COMMENT 'Description of Keep' NOT NULL,
  Img VARCHAR(500) COMMENT "Keep's Image URL",
  Views INT NOT NULL DEFAULT 0,
  CreatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (CreatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';



DROP TABLE keeps;
-- USE TO NUKE THE DARN KEEPS TABLE IF NEEDED :) OTHERWISE, do NOT HIT THIS BUTTON

SELECT *
FROM keeps
JOIN accounts creator ON keeps.CreatorId = creator.id GROUP BY keeps.id;



INSERT INTO keeps
(`Name`, `Description`, `Img`, `CreatorId` )
VALUES
('Box Dweller', 'Why he in the boxy dough? What terrible unspeakable horrors has thou committed to be entrenched into such a fate?','https://images.unsplash.com/photo-1498579687545-d5a4fffb0a9e?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80','64233077a1188ee878fee3ca' );

INSERT INTO keeps
(`Name`, `Description`, `Img`, `CreatorId` )
VALUES
('KITTEN', 'His soul is lit afire, let us hope you are not the target of his gaze lest thou feels the scrouge of the one they call kitten','https://images.unsplash.com/photo-1535467487981-c86a98db6b9c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80','64233077a1188ee878fee3ca' )


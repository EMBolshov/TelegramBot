CREATE TABLE public."Chords"
(
    "Id" SERIAL PRIMARY KEY,
    "Name" character varying (8) NOT NULL,
    "Fingering" character varying (255) NOT NULL 
)
WITH (
    OIDS = FALSE
    );

ALTER TABLE public."Chords"
    OWNER to nahageixgwxfnl;

CREATE TABLE public."Songs"
(
    "Id" SERIAL PRIMARY KEY,
    "Name" character varying (127) NOT NULL,
    "Beat" character varying (127),
    "Chords" character varying (127) NOT NULL,
    "Capo" character varying (8),
    "Text" text    
)
WITH (
    OIDS = FALSE
    );

ALTER TABLE public."Songs"
    OWNER to nahageixgwxfnl;

CREATE TABLE public."Logs"
(
    "Id" serial,
    "Timestamp" timestamp,
    "Level" varchar,
    "Message" text
)
    WITH (
        OIDS = FALSE
        );

ALTER TABLE public."Logs"
    OWNER to nahageixgwxfnl;
	
CREATE TABLE public."Chords"
(
    "Id" SERIAL,
    "Name" character varying (8) NOT NULL,
    "Fingering" character varying (255) NOT NULL 
)
WITH (
    OIDS = FALSE
    );

ALTER TABLE public."Chords"
    OWNER to rxkoxwvrchcimp;

CREATE TABLE public."Songs"
(
    "Id" SERIAL,
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
    OWNER to rxkoxwvrchcimp;


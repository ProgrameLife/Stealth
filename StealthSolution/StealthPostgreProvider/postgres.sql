-- Database: stealthdb
-- DROP DATABASE stealthdb;
CREATE DATABASE stealthdb
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Chinese (Simplified)_China.936'
    LC_CTYPE = 'Chinese (Simplified)_China.936'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;


-- Table: public.stealthstatus
-- DROP TABLE public.stealthstatus;
CREATE TABLE public.stealthstatus
(
    keyname character varying(128) COLLATE pg_catalog."default" NOT NULL,
    status integer,
    modifytime time without time zone,
    CONSTRAINT "StealthStatus_pkey" PRIMARY KEY (keyname)
)
WITH (
    OIDS = FALSE)
TABLESPACE pg_default;
ALTER TABLE public.stealthstatus
    OWNER to postgres;


-- Table: public.quartzsettings
-- DROP TABLE public.quartzsettings;
CREATE TABLE public.quartzsettings
(
    id serial,
    name character varying(512) COLLATE pg_catalog."default",
    typename character varying(128) COLLATE pg_catalog."default",
    cronexpression character varying(32) COLLATE pg_catalog."default",
    validate boolean,
    CONSTRAINT quartzsettings_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE public.quartzsettings
    OWNER to postgres;
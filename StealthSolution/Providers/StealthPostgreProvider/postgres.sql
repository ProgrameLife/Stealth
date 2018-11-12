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



-- Table: public.datasettings
-- DROP TABLE public.datasettings;
CREATE TABLE public.datasettings
(
    id serial,
    keyname character varying(128) COLLATE pg_catalog."default",
    connectionstring text COLLATE pg_catalog."default",
    groupno character varying(64) COLLATE pg_catalog."default",
    validate boolean,
    createon timestamp without time zone,
    CONSTRAINT datasettings_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.datasettings
    OWNER to postgres;



-- Table: public.datasqls
-- DROP TABLE public.datasqls;
CREATE TABLE public.datasqls
(
    id serial,
    datasettingid integer,
    sql text COLLATE pg_catalog."default",
    transactionno character varying(64) COLLATE pg_catalog."default",
    groupno character varying(64) COLLATE pg_catalog."default",
    validate boolean,
    createon timestamp without time zone,
    CONSTRAINT datasqls_pkey PRIMARY KEY (id),
    CONSTRAINT datasettingid_fk FOREIGN KEY (id)
        REFERENCES public.datasettings (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.datasqls
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
    createon timestamp without time zone,
    CONSTRAINT quartzsettings_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.quartzsettings
    OWNER to postgres;



-- Table: public.sftpettings
-- DROP TABLE public.sftpettings;
CREATE TABLE public.sftpettings
(
    id serial,
    host character varying(128) COLLATE pg_catalog."default",
    port integer,
    username character varying(128) COLLATE pg_catalog."default",
    password character varying(128) COLLATE pg_catalog."default",
    certificatepath character varying(256) COLLATE pg_catalog."default",
    transferdirectory character varying(256) COLLATE pg_catalog."default",
    transferfileprefix character varying(256) COLLATE pg_catalog."default",
    validate boolean,
    createon timestamp without time zone,
    CONSTRAINT sftpettings_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.sftpettings
    OWNER to postgres;
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



CREATE TABLE "public"."quartzsettings" (
	"id" serial PRIMARY KEY, 
	"keyname" varchar(512), 
	"typename" varchar(128), 
	"cronexpression" varchar(32), 
	"validate" boolean, 
	"createon" timestamp(6)
);

CREATE UNIQUE INDEX "keyname"
	ON "public"."quartzsettings"
	USING btree (keyname);



CREATE TABLE "public"."datasettings" (
	"id" serial PRIMARY KEY, 
	"keyname" varchar(128), 
	"connectionstring" text, 
	"groupno" varchar(64), 
	"validate" boolean, 
	"createon" timestamp(6)
);

CREATE TABLE "public"."datasqls" (
	"id" serial PRIMARY KEY, 
	"keyname" varchar(512), 
	"datasettingid" integer, 
	"sql" text, 
	"transactionno" varchar(64), 
	"groupno" varchar(64), 
	"validate" boolean, 
	"createon" timestamp(6), 
	FOREIGN KEY ("id")
		REFERENCES "public"."datasettings" ("id")
		ON UPDATE NO ACTION ON DELETE NO ACTION
);


CREATE TABLE "public"."emailsettings" (
	"id" serial PRIMARY KEY, 
	"keyname" varchar(512), 
	"host" varchar(256), 
	"port" integer, 
	"fromaddress" varchar(256), 
	"username" varchar(256), 
	"password" varchar(256), 
	"subject" varchar(128), 
	"body" text, 
	"toaddresses" text, 
	"iscompress" boolean, 
	"compressfile" varchar(254), 
	"compresspassword" varchar(254), 
	"isattachment" boolean, 
	"attachmentname" varchar(512), 
	"validate" boolean, 
	"createon" timestamp(6) DEFAULT now()
);


CREATE TABLE "public"."sftpettings" (
	"id" serial PRIMARY KEY, 
	"keyname" varchar(512), 
	"host" varchar(128), 
	"port" integer, 
	"username" varchar(128), 
	"password" varchar(128), 
	"certificatepath" varchar(256), 
	"transferdirectory" varchar(256), 
	"transferfileprefix" varchar(256), 
	"filename" varchar(512), 
	"createon" timestamp(6), 
	"validate" boolean
);

CREATE TABLE "public"."stealthstatus" (
	"keyname" varchar(128) NOT NULL PRIMARY KEY, 
	"status" integer, 
	"modifytime" time(6)
);

CREATE TABLE dbo.report
(
    id                    int IDENTITY,
    meta_id               int NULL,
    settlement            varchar(100) NULL,
    transaction_credit    decimal(18, 2) NULL,
    reconciliation_credit decimal(18, 2) NULL,
    fee_credit            decimal(18, 2) NULL,
    transaction_debit     decimal(18, 2) NULL,
    reconciliation_debit  decimal(18, 2) NULL,
    fee_debit             decimal(18, 2) NULL,
    count                 decimal(18, 2) NULL,
    net                   decimal(18, 2) NULL,
    CONSTRAINT PK_table1_id PRIMARY KEY CLUSTERED (id)
)


CREATE TABLE dbo.meta
(
    id                      int IDENTITY,
    institution             varchar(50) NULL,
    file_id                 varchar(50) NULL,
    date                    date NULL,
    transaction_currency    varchar(50) NULL,
    reconciliation_currency varchar(50) NULL,
    CONSTRAINT PK_meta_id PRIMARY KEY CLUSTERED (id)
)

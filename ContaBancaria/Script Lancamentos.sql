USE [BancoConta]
GO

/****** Object:  Table [dbo].[Lancamentos]    Script Date: 04/12/2023 21:56:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Lancamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContaId] [int] NULL,
	[Valor] [decimal](18, 2) NULL,
	[Data] [datetime] NULL,
	[TipoOperacao] [int] NULL,
 CONSTRAINT [PK__Lancamen__3214EC07B72029ED] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lancamentos]  WITH CHECK ADD  CONSTRAINT [FK__Lancament__Conta__398D8EEE] FOREIGN KEY([ContaId])
REFERENCES [dbo].[Contas] ([Id])
GO

ALTER TABLE [dbo].[Lancamentos] CHECK CONSTRAINT [FK__Lancament__Conta__398D8EEE]
GO


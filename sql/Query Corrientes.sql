/****** Script for SelectTopNRows command from SSMS  ******/
use nx_data;

SELECT * FROM 
	corrientes_historial 
	LEFT JOIN rectificadores ON (corrientes_historial.rectificador = rectificadores.id)
	LEFT JOIN product_description ON (corrientes_historial.oid = product_description.oid)
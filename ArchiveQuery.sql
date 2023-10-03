
QUERY A: select PRODUCTO.DESCRIPCION from PRODUCTO_PROYECTO 
inner join PRODUCTO on PRODUCTO_PROYECTO.PRODUCTO = PRODUCTO.PRODUCTO
where proyecto = 1

QUERY B: SELECT PR.NOMBRE, PRO.DESCRIPCION, MSJ.COD_FORMATO  FROM MENSAJE MSJ
LEFT JOIN PRODUCTO PR ON PR.PRODUCTO = MSJ.PRODUCTO
LEFT JOIN PROYECTO PRO ON PRO.PROYECTO = MSJ.PROYECTO


QUERY C: SELECT
    FM.ASUNTO AS MENSAJE,
    PR.NOMBRE AS PROYECTO,
    CASE
        WHEN COUNT(DISTINCT M.PRODUCTO) = (SELECT COUNT(DISTINCT PRODUCTO) FROM MENSAJE WHERE PROYECTO = M.PROYECTO) THEN 'TODOS'
        ELSE GROUP_CONCAT(DISTINCT PR.NOMBRE ', ')
    END AS PRODUCTO
FROM FORMATO_MENSAJE FM
INNER JOIN MENSAJE M ON FM.COD_FORMATO = M.COD_FORMATO
LEFT JOIN PROYECTO PR ON M.PROYECTO = PR.PROYECTO
LEFT JOIN PRODUCTO P ON M.PRODUCTO = P.PRODUCTO
GROUP BY FM.COD_TIPO

		

#Agregar migracion 
add-migration Inicial -Context ComercioElectronicoDbContext

##aplicar migracion 
Update-Database -Context ComercioElectronicoDbContext

#realizar migracion por script 
Script-Migration -Context ComercioElectronicoDbContext -From Inicial 

#Genera script desde la primera migracion 
Script-Migration -Context ComercioElectronicoDbContext 0
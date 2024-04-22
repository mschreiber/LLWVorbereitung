
## Aufgabe 1

Welche Mitarbeiter arbeiten in der Abteilung mit der id 1

```sql
select * from employees where department_id = 1

-- 5 rows
```

## Aufgabe 2

Gib die Projektnamen der Projekte mit der id 5-7 aus

```sql
select name from projects where project_id between 5 and 7

-- 3 rows
```
-- alternativ
```sql
select name from projects where project_id in (5,6,7)

-- 3 rows
```
## Aufgabe 3

Finde alle Namen der Projekte mit einem Projektbudget von mehr als 100.000 und weniger wie 200.000
Die Ausgabe soll absteigend nach dem Budget sein

```sql
select name from projects where budget > 100000 and budget < 200000
order by budget desc

-- 2 rows
-- Projekt J und G
```

## Aufgabe 4

Finde die Projekte, bei denen der/die ProjektleiterIn "David" oder "Sarah" heisen

```sql
select p.name from projects p 
inner join employees e on p.leader_id = e.employee_id
where e.firstname = "David" or e.firstname = "Sarah"

-- 2 rows
-- Projekt G und H
```

## Aufgabe 5

Wieviel Stunden wurden bisher für das Projekt "Project G" aufgewendet?

```sql
select sum(pt.spent_hours) from project_times pt
inner join projects p on p.project_id = pt.project_id
where p.name = "Project G"
-- 1 row
-- 31,94
```

## Aufgabe 6

Gib die Anzahl an Zeitbuchungen, die Summe an Stunden und der Durchschnitt an gebuchten Stunden pro 
Projekt und pro Abteilung zurück. Ausgegeben werden sollen, Projektname, Name der Abteilung, Anzahl Buchungen,
Summe der Stunden, Durchschnitt der Stunden, sortiert nach Anzahl der Buchungen aufsteigend

```sql
SELECT 
    p.name AS project_name,
    d.name AS department_name,
    COUNT(pt.project_time_id) AS total_time_entries,
    SUM(pt.spent_hours) AS total_hours_spent,
    AVG(pt.spent_hours) AS average_hours_per_entry
FROM 
    projects p
JOIN 
    project_times pt ON p.project_id = pt.project_id
JOIN 
    employees e ON pt.employee_id = e.employee_id
JOIN 
    departments d ON e.department_id = d.department_id
GROUP BY 
    p.project_id, d.department_id
ORDER BY 
    p.project_id, d.department_id;
-- 27 row
```
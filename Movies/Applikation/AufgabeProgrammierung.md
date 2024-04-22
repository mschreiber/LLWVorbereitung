---
title: LLW - Programmierung
date: 29. August 2023
documentclass: exam
links-as-notes: true
numbersections: true
secnumdepth: 2
---

Das Ziel der Aufgabe ist der Erstellen einer Verwaltung von Filmen.

# Requirements

Ein Film besteht aus den folgenden Daten:

- einem Filmtitel (nicht leer, maximal 100 Zeichen),
- einem Release Datum (unix timestamp, darf maximal 1 Jahr in der Zukunft liegen),
- einer optionalen Bewertung (1 bis 5 Sterne),
- einem Budget in USD (darf nicht negativ sein),
- dem Umsatz im ersten Jahr (USD),
- dem Genre des Films,
- und dem Namen des Regisseurs (nicht leer, maximal 100 Zeichen).

Das Genre des Films **muss** eines der folgenden gültigen Genres sein:

- "Comedy",
- "Action",
- "Documentary",
- "Drama"
- oder "Romantic".

## Duplikate Filme

Zwei Filme bei denen alle oben genannten Daten ident sind gilt als Duplikat und es soll nicht möglich sein, dass ein Filme doppelt vorkommen.

Wenn sich hingegen mindestens ein Wert unterscheidet gilt dies nicht als Duplikat.

Zulässig ist also z.B. die Neuveröffentlichung eines Filmes, mit den gleichen Schauspielern aber unterschiedlichem Erscheinungsdatum.

## UI

Das Programm soll eine User Interface anbieten mit dem der Benutzer die Filme verwalten kann.

Dabei sollen die Grundfunktionalitäten 

- Auflisten aller bestehenden Filme,
- Ändern von Daten eines bestehenden Films,
- Löschen eines bestehenden Films,
- und das Erstellen eines neuen Films

möglich sein.

Wird die Applikation/Anwendung gestartet soll die erste Ansicht eine Auflistung der vorhandenen Filme sein.
In der Filmliste soll zumindest der Titel und das Erscheinungsdatum der Filme angezeigt werden.
Es können aber auch mehr oder alle Eigenschaften angezeigt werden.

Es soll möglich sein einen Film in der Liste zu selektieren und den selektierten Film zu bearbeiten oder zu löschen.

Das Löschen eines Films soll durch eine zusätzliche Abfrage bestätigt werden.

Beim Erstellen oder Bearbeiten eines Films muss beachtet werden dass keine Duplikate gespeichert werden dürfen.

Weiters soll es nicht möglich sein, ungültige/invalide Werte zu speichern (zb negatives Budget).

## Usability

Die Anwendung soll einfach bedienbar und klar verständlich sein (im Rahmen der begrenzten zeitlichen Möglichkeiten).
Die verwendeten Texte sollen einheitlich Deutsch oder Englisch sein.

## Persistenz

Die Daten sollen so gespeichert werden, dass sie bei jedem Start des Programms wieder zur Verfügung stehen.

Security Aspekte wie das Verhindern von SQL-injection müssen berücksichtigt werden.

Eine Beispieldatenbank im SQLite und Sql Format ist gegeben, muss aber nicht zwingend verwendet werden.

# Technische Vorgaben

Programmiersprache ist frei wählbar (z.B. C# mit WPF oder Windows Forms).

Der Quellcode soll klar strukturiert und **ausreichend mit Kommentaren versehen** sein (Funktionen/Methoden/Parameter/Rückgabewerte).

# Testen

Die Applikation soll auf ihre vollständige Funktionalität hin geprüft werden, dies muss aber nicht dokumentiert werden sondern soll nur der eigenen Fehlerdiagnose dienen.

Weiters werden keine Automatisierte Tests (z.B. Unit Tests) verlangt.

# Dokumentation und Abgabe

Die Abgabe **muss ein Readme Datei beinhalten**.
Darin soll kurz beschrieben werden wie die Prüfer das Programm auf ihren eigenen Rechnern starten können.

# Hilfsmittel

- Die Nutzung des Internets ist z.B. für das Lesen von Dokumentation, Recherche oder herunterladen von dependencies erlaubt.
- Die Übernahme von Lösungen oder Teillösungen ist jedoch streng untersagt.
- Die IT-Infrastruktur wird von der HTL Rankweil gestellt. Eigene Laptops sind nicht erlaubt und auch USB-Sticks werden keine benötigt.
- in der Datei `RESEARCH.md` zur Verfügung gestellten Informationen und Codeteile dürfen verwendet werden.

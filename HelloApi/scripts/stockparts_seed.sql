CREATE TABLE StockPieces (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,              -- Identifiant unique de la pièce
    CodePiece VARCHAR(50) NOT NULL UNIQUE,             -- Code interne ou référence
    NomPiece VARCHAR(150) NOT NULL,                    -- Désignation de la pièce
    Description TEXT,                                  -- Description détaillée
    QuantiteActuelle INTEGER NOT NULL DEFAULT 0,       -- Quantité disponible en stock
    QuantiteMinimale INTEGER NOT NULL DEFAULT 0,       -- Seuil d’alerte de réapprovisionnement
    PrixUnitaire DECIMAL(10, 2) NOT NULL DEFAULT 0.0,  -- Prix d'achat unitaire
    Localisation VARCHAR(100),                         -- Emplacement (ex: "Rayon A3 - Bac 12")
    Fournisseur VARCHAR(100),                          -- Nom du fournisseur principal
    DateDerniereEntree DATETIME,                       -- Dernière date d’approvisionnement
    DateDerniereSortie DATETIME,                       -- Dernière date de sortie
    EnService BOOLEAN NOT NULL DEFAULT 1,              -- Statut actif/inactif de la pièce
    DateCreation DATETIME DEFAULT CURRENT_TIMESTAMP,   -- Date d’insertion
    DateModification DATETIME                          -- Dernière mise à jour
);


CREATE TABLE StockParts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,              -- Unique identifier
    PartCode VARCHAR(50) NOT NULL UNIQUE,              -- Internal or supplier reference
    PartName VARCHAR(150) NOT NULL,                    -- Part name or designation
    Description TEXT,                                  -- Detailed description
    CurrentStock INTEGER NOT NULL DEFAULT 0,           -- Current available quantity
    MinimalStock INTEGER NOT NULL DEFAULT 0,           -- Minimum stock threshold
    UnitPrice DECIMAL(10, 2) NOT NULL DEFAULT 0.0,     -- Unit purchase price
    Location VARCHAR(100),                             -- Physical location in warehouse
    Supplier VARCHAR(100),                             -- Main supplier name
    LastEntryDate DATETIME,                            -- Last restock date
    LastExitDate DATETIME,                             -- Last withdrawal date
    IsActive BOOLEAN NOT NULL DEFAULT 1,               -- Whether part is active or not
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,      -- Record creation date
    UpdatedAt DATETIME                                 -- Last modification date
);

-- Seed data for StockParts (80 references: ~40 Schneider Electric, ~40 KWS/SKF)
BEGIN TRANSACTION;
INSERT INTO StockParts (PartCode, PartName, Description, CurrentStock, MinimalStock, UnitPrice, Location, Supplier, LastEntryDate) VALUES
('SCH-A9F7406', 'Miniature Circuit Breaker 6A', 'Acti9 iC60N 1P+N 6A C Curve MCB', 25, 5, 15.40, 'E1-A1', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7410', 'Miniature Circuit Breaker 10A', 'Acti9 iC60N 1P+N 10A C Curve MCB', 20, 10, 19.00, 'E1-A2', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7416', 'Miniature Circuit Breaker 16A', 'Acti9 iC60N 1P+N 16A C Curve MCB', 25, 10, 24.40, 'E1-A2', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7420', 'Miniature Circuit Breaker 20A', 'Acti9 iC60N 1P+N 20A C Curve MCB', 20, 15, 28.00, 'E1-A3', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7425', 'Miniature Circuit Breaker 25A', 'Acti9 iC60N 1P+N 25A C Curve MCB', 20, 10, 32.50, 'E1-A2', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7432', 'Miniature Circuit Breaker 32A', 'Acti9 iC60N 1P+N 32A C Curve MCB', 30, 15, 38.80, 'E1-A3', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7440', 'Miniature Circuit Breaker 40A', 'Acti9 iC60N 1P+N 40A C Curve MCB', 20, 10, 46.00, 'E1-A2', 'Schneider Electric', '2025-09-28'),
('SCH-A9F7463', 'Miniature Circuit Breaker 63A', 'Acti9 iC60N 1P+N 63A C Curve MCB', 35, 5, 66.70, 'E1-A1', 'Schneider Electric', '2025-09-28'),
('SCH-LC1D09', 'Contactor 9A 230V AC', 'TeSys D contactor, 3P, 9A, coil 230V AC', 24, 6, 33.70, 'E1-A1', 'Schneider Electric', '2025-09-27'),
('SCH-LC1D12', 'Contactor 12A 230V AC', 'TeSys D contactor, 3P, 12A, coil 230V AC', 18, 6, 37.60, 'E1-A1', 'Schneider Electric', '2025-09-27'),
('SCH-LC1D18', 'Contactor 18A 230V AC', 'TeSys D contactor, 3P, 18A, coil 230V AC', 30, 6, 45.40, 'E1-A1', 'Schneider Electric', '2025-09-27'),
('SCH-LC1D25', 'Contactor 25A 230V AC', 'TeSys D contactor, 3P, 25A, coil 230V AC', 24, 6, 54.50, 'E1-A2', 'Schneider Electric', '2025-09-27'),
('SCH-LC1D32', 'Contactor 32A 230V AC', 'TeSys D contactor, 3P, 32A, coil 230V AC', 18, 6, 63.60, 'E1-A3', 'Schneider Electric', '2025-09-27'),
('SCH-LC1D38', 'Contactor 38A 230V AC', 'TeSys D contactor, 3P, 38A, coil 230V AC', 30, 6, 71.40, 'E1-A3', 'Schneider Electric', '2025-09-27'),
('SCH-ZB5AA3', 'Push Button Head Green Ø22', 'Harmony XB5, flush, momentary, Green', 80, 20, 4.20, 'E3-B2', 'Schneider Electric', '2025-09-25'),
('SCH-ZB5AA4', 'Push Button Head Red Ø22', 'Harmony XB5, flush, momentary, Red', 80, 20, 4.20, 'E3-B2', 'Schneider Electric', '2025-09-25'),
('SCH-ZB5AA5', 'Push Button Head Yellow Ø22', 'Harmony XB5, flush, momentary, Yellow', 80, 20, 4.20, 'E3-B2', 'Schneider Electric', '2025-09-25'),
('SCH-ZB5AA6', 'Push Button Head Blue Ø22', 'Harmony XB5, flush, momentary, Blue', 80, 20, 4.20, 'E3-B2', 'Schneider Electric', '2025-09-25'),
('SCH-ZB5AA7', 'Push Button Head Black Ø22', 'Harmony XB5, flush, momentary, Black', 80, 20, 4.20, 'E3-B2', 'Schneider Electric', '2025-09-25'),
('SCH-ZB5AA9', 'Push Button Head White Ø22', 'Harmony XB5, flush, momentary, White', 80, 20, 4.20, 'E3-B2', 'Schneider Electric', '2025-09-25'),
('SCH-ATV12H037M2', 'Variable Speed Drive 0.37kW', 'Altivar 12, 0.37kW, 230V single-phase', 10, 2, 212.20, 'E2-A1', 'Schneider Electric', '2025-09-21'),
('SCH-ATV12H055M2', 'Variable Speed Drive 0.55kW', 'Altivar 12, 0.55kW, 230V single-phase', 10, 2, 223.00, 'E2-A1', 'Schneider Electric', '2025-09-21'),
('SCH-ATV12H075M2', 'Variable Speed Drive 0.75kW', 'Altivar 12, 0.75kW, 230V single-phase', 10, 2, 235.00, 'E2-A1', 'Schneider Electric', '2025-09-21'),
('SCH-ATV12H110M2', 'Variable Speed Drive 1.1kW', 'Altivar 12, 1.1kW, 230V single-phase', 10, 2, 256.00, 'E2-A1', 'Schneider Electric', '2025-09-21'),
('SCH-ATV12H150M2', 'Variable Speed Drive 1.5kW', 'Altivar 12, 1.5kW, 230V single-phase', 10, 2, 280.00, 'E2-A1', 'Schneider Electric', '2025-09-21'),
('SCH-ATV12H220M2', 'Variable Speed Drive 2.2kW', 'Altivar 12, 2.2kW, 230V single-phase', 10, 2, 322.00, 'E2-A1', 'Schneider Electric', '2025-09-21'),
('SCH-LADN22', 'Auxiliary Contact Block', 'Front mounting, 2 NO + 2 NC contacts', 70, 10, 8.90, 'E1-A3', 'Schneider Electric', '2025-09-26'),
('SCH-XCSDMP7002', 'Safety Switch Magnetically Coded', 'XCSDMP coded magnetic safety switch, 2NC', 24, 5, 65.00, 'E3-B1', 'Schneider Electric', '2025-09-26'),
('SCH-NSX100N', 'Molded Case Circuit Breaker 100A', 'Compact NSX100N, 3P, 100A, thermal-magnetic', 8, 2, 185.00, 'E4-A1', 'Schneider Electric', '2025-09-26'),
('SCH-XMLB004A2S11', 'Pressure Sensor 0–4 bar', 'XMLB analog pressure transmitter, 24V DC', 14, 3, 96.50, 'E4-A2', 'Schneider Electric', '2025-09-26'),
('SCH-RM17TE00', 'Temperature Control Relay', 'Zelio Control, temperature monitoring 24–240V', 18, 4, 42.00, 'E5-B1', 'Schneider Electric', '2025-09-26'),
('SCH-XB5AVB3', 'Selector Switch 3-Position', 'Harmony XB5, maintained, black knob', 26, 5, 12.40, 'E3-B3', 'Schneider Electric', '2025-09-26'),
('SCH-ZELIO-SR2B121BD', 'Logic Relay 12 I/O 24VDC', 'Zelio smart relay, 12 I/O, 24V DC', 9, 2, 85.00, 'E5-A2', 'Schneider Electric', '2025-09-26'),
('SCH-ABL8MEM24012', 'Power Supply 24V 12A', 'Switch mode power supply 24VDC 12A', 6, 2, 120.00, 'E2-B1', 'Schneider Electric', '2025-09-26'),
('SCH-XS1M12PA370', 'Proximity Sensor M12 PNP NO', 'Inductive prox sensor, 12mm, PNP NO', 40, 8, 22.50, 'E4-B2', 'Schneider Electric', '2025-09-26'),
('SCH-TSXDMZ64DTK', 'PLC Digital Output Module', 'Modicon module, 64 DO, transistor', 5, 1, 210.00, 'E2-B2', 'Schneider Electric', '2025-09-26'),
('SCH-LR2D13', 'Thermal Overload Relay 0.63–1A', 'TeSys D thermal overload relay', 16, 4, 28.50, 'E1-B1', 'Schneider Electric', '2025-09-26'),
('SCH-ZB4BZ102', 'Contact Block 1NO', 'Harmony XB4 contact block, 1 NO', 90, 20, 3.30, 'E3-A1', 'Schneider Electric', '2025-09-26'),
('SCH-RE22R1AMR', 'Timing Relay Multifunction', 'RE22, 24–240V AC/DC, 1CO, 0.1s–100h', 12, 3, 39.90, 'E5-A1', 'Schneider Electric', '2025-09-26'),
('SCH-VDI9C12', 'DIN Rail End Clamp', 'Universal end clamp for DIN rail, pack of 20', 50, 10, 7.80, 'E3-C1', 'Schneider Electric', '2025-09-26'),
('KWS-6004ZZ', 'Ball Bearing 6004ZZ', 'Single-row deep groove ball bearing, double shielded. 20x42x12', 30, 10, 2.50, 'M1-A1', 'KWS', '2025-09-24'),
('KWS-6005ZZ', 'Ball Bearing 6005ZZ', 'Single-row deep groove ball bearing, double shielded. 25x47x12', 35, 15, 3.30, 'M2-A2', 'KWS', '2025-09-24'),
('KWS-6006ZZ', 'Ball Bearing 6006ZZ', 'Single-row deep groove ball bearing, double shielded. 30x55x13', 40, 20, 4.10, 'M3-A3', 'KWS', '2025-09-24'),
('KWS-6007ZZ', 'Ball Bearing 6007ZZ', 'Single-row deep groove ball bearing, double shielded. 35x62x14', 45, 10, 4.90, 'M1-A4', 'KWS', '2025-09-24'),
('KWS-6008ZZ', 'Ball Bearing 6008ZZ', 'Single-row deep groove ball bearing, double shielded. 40x68x15', 50, 15, 5.70, 'M2-A1', 'KWS', '2025-09-24'),
('KWS-6203RS', 'Ball Bearing 6203RS', 'Deep groove ball bearing, rubber sealed. 17x40x12', 55, 20, 6.50, 'M3-A2', 'KWS', '2025-09-24'),
('KWS-6204RS', 'Ball Bearing 6204RS', 'Deep groove ball bearing, rubber sealed. 20x47x14', 60, 10, 7.30, 'M1-A3', 'KWS', '2025-09-24'),
('KWS-6205RS', 'Ball Bearing 6205RS', 'Deep groove ball bearing, rubber sealed. 25x52x15', 30, 15, 8.10, 'M2-A4', 'KWS', '2025-09-24'),
('KWS-6206RS', 'Ball Bearing 6206RS', 'Deep groove ball bearing, rubber sealed. 30x62x16', 35, 20, 8.90, 'M3-A1', 'KWS', '2025-09-24'),
('KWS-6207RS', 'Ball Bearing 6207RS', 'Deep groove ball bearing, rubber sealed. 35x72x17', 40, 10, 9.70, 'M1-A2', 'KWS', '2025-09-24'),
('KWS-6208RS', 'Ball Bearing 6208RS', 'Deep groove ball bearing, rubber sealed. 40x80x18', 45, 15, 2.50, 'M2-A3', 'KWS', '2025-09-24'),
('KWS-6209RS', 'Ball Bearing 6209RS', 'Deep groove ball bearing, rubber sealed. 45x85x19', 50, 20, 3.30, 'M3-A4', 'KWS', '2025-09-24'),
('KWS-6210ZZ', 'Ball Bearing 6210ZZ', 'Deep groove bearing, double shielded. 50x90x20', 55, 10, 4.10, 'M1-A1', 'KWS', '2025-09-24'),
('KWS-6305ZZ', 'Ball Bearing 6305ZZ', 'Deep groove ball bearing, metal shields. 25x62x17', 60, 15, 4.90, 'M2-A2', 'KWS', '2025-09-24'),
('KWS-6306ZZ', 'Ball Bearing 6306ZZ', 'Deep groove ball bearing, metal shields. 30x72x19', 30, 20, 5.70, 'M3-A3', 'KWS', '2025-09-24'),
('KWS-6307ZZ', 'Ball Bearing 6307ZZ', 'Deep groove ball bearing, metal shields. 35x80x21', 35, 10, 6.50, 'M1-A4', 'KWS', '2025-09-24'),
('KWS-6308ZZ', 'Ball Bearing 6308ZZ', 'Deep groove ball bearing, metal shields. 40x90x23', 40, 15, 7.30, 'M2-A1', 'KWS', '2025-09-24'),
('KWS-6309ZZ', 'Ball Bearing 6309ZZ', 'Deep groove ball bearing, metal shields. 45x100x25', 45, 20, 8.10, 'M3-A2', 'KWS', '2025-09-24'),
('KWS-6310ZZ', 'Ball Bearing 6310ZZ', 'Deep groove ball bearing, metal shields. 50x110x27', 50, 10, 8.90, 'M1-A3', 'KWS', '2025-09-24'),
('SKF-6004ZZ', 'Ball Bearing 6004ZZ', 'Single-row deep groove ball bearing, double shielded. 20x42x12', 55, 15, 9.70, 'M2-A4', 'SKF', '2025-09-24'),
('SKF-6005ZZ', 'Ball Bearing 6005ZZ', 'Single-row deep groove ball bearing, double shielded. 25x47x12', 60, 20, 2.50, 'M3-A1', 'SKF', '2025-09-24'),
('SKF-6006ZZ', 'Ball Bearing 6006ZZ', 'Single-row deep groove ball bearing, double shielded. 30x55x13', 30, 10, 3.30, 'M1-A2', 'SKF', '2025-09-24'),
('SKF-6007ZZ', 'Ball Bearing 6007ZZ', 'Single-row deep groove ball bearing, double shielded. 35x62x14', 35, 15, 4.10, 'M2-A3', 'SKF', '2025-09-24'),
('SKF-6008ZZ', 'Ball Bearing 6008ZZ', 'Single-row deep groove ball bearing, double shielded. 40x68x15', 40, 20, 4.90, 'M3-A4', 'SKF', '2025-09-24'),
('SKF-6203RS', 'Ball Bearing 6203RS', 'Deep groove ball bearing, rubber sealed. 17x40x12', 45, 10, 5.70, 'M1-A1', 'SKF', '2025-09-24'),
('SKF-6204RS', 'Ball Bearing 6204RS', 'Deep groove ball bearing, rubber sealed. 20x47x14', 50, 15, 6.50, 'M2-A2', 'SKF', '2025-09-24'),
('SKF-6205RS', 'Ball Bearing 6205RS', 'Deep groove ball bearing, rubber sealed. 25x52x15', 55, 20, 7.30, 'M3-A3', 'SKF', '2025-09-24'),
('SKF-6206RS', 'Ball Bearing 6206RS', 'Deep groove ball bearing, rubber sealed. 30x62x16', 60, 10, 8.10, 'M1-A4', 'SKF', '2025-09-24'),
('SKF-6207RS', 'Ball Bearing 6207RS', 'Deep groove ball bearing, rubber sealed. 35x72x17', 30, 15, 8.90, 'M2-A1', 'SKF', '2025-09-24'),
('SKF-6208RS', 'Ball Bearing 6208RS', 'Deep groove ball bearing, rubber sealed. 40x80x18', 35, 20, 9.70, 'M3-A2', 'SKF', '2025-09-24'),
('SKF-6209RS', 'Ball Bearing 6209RS', 'Deep groove ball bearing, rubber sealed. 45x85x19', 40, 10, 2.50, 'M1-A3', 'SKF', '2025-09-24'),
('SKF-6210ZZ', 'Ball Bearing 6210ZZ', 'Deep groove bearing, double shielded. 50x90x20', 45, 15, 3.30, 'M2-A4', 'SKF', '2025-09-24'),
('SKF-6305ZZ', 'Ball Bearing 6305ZZ', 'Deep groove ball bearing, metal shields. 25x62x17', 50, 20, 4.10, 'M3-A1', 'SKF', '2025-09-24'),
('SKF-6306ZZ', 'Ball Bearing 6306ZZ', 'Deep groove ball bearing, metal shields. 30x72x19', 55, 10, 4.90, 'M1-A2', 'SKF', '2025-09-24'),
('SKF-6307ZZ', 'Ball Bearing 6307ZZ', 'Deep groove ball bearing, metal shields. 35x80x21', 60, 15, 5.70, 'M2-A3', 'SKF', '2025-09-24'),
('SKF-6308ZZ', 'Ball Bearing 6308ZZ', 'Deep groove ball bearing, metal shields. 40x90x23', 30, 20, 6.50, 'M3-A4', 'SKF', '2025-09-24'),
('SKF-6309ZZ', 'Ball Bearing 6309ZZ', 'Deep groove ball bearing, metal shields. 45x100x25', 35, 10, 7.30, 'M1-A1', 'SKF', '2025-09-24'),
('SKF-6310ZZ', 'Ball Bearing 6310ZZ', 'Deep groove ball bearing, metal shields. 50x110x27', 40, 15, 8.10, 'M2-A2', 'SKF', '2025-09-24'),
('KWS-UCFL204', 'Flanged Bearing UCFL204', '2-bolt flange housing bearing', 45, 20, 8.90, 'M3-A3', 'KWS', '2025-09-24'),
('KWS-UCP204', 'Pillow Block Bearing UCP204', 'Pillow block housing bearing', 50, 10, 9.70, 'M1-A4', 'KWS', '2025-09-24');
COMMIT;
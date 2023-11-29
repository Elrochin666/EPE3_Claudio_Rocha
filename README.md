CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Medico`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Medico` (
  `idMedico` INT NOT NULL,
  `NombreMed` VARCHAR(50) NULL,
  `ApellidoMed` VARCHAR(50) NULL,
  `RunMed` VARCHAR(50) NULL,
  `Eunacom` VARCHAR(5) NULL,
  `NacionalidadMed` VARCHAR(45) NULL,
  `Especialidad` VARCHAR(45) NULL,
  `Horarios` TIME NULL,
  `TarifaHr` INT NULL,
  PRIMARY KEY (`idMedico`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Paciente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Paciente` (
  `idPaciente` INT NOT NULL,
  `NombrePac` VARCHAR(50) NULL,
  `ApellidoPac` VARCHAR(50) NULL,
  `RunPac` VARCHAR(25) NULL,
  `Nacionalidad` VARCHAR(50) NULL,
  `Visa` VARCHAR(5) NULL,
  `Genero` VARCHAR(10) NULL,
  `SintomasPac` VARCHAR(100) NULL,
  `Medico_idMedico` INT NOT NULL,
  PRIMARY KEY (`idPaciente`),
  INDEX `fk_Paciente_Medico1_idx` (`Medico_idMedico`),
  CONSTRAINT `fk_Paciente_Medico1`
    FOREIGN KEY (`Medico_idMedico`)
    REFERENCES `mydb`.`Medico` (`idMedico`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Reserva`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Reserva` (
  `idReserva` INT NOT NULL,
  `Especialidad` VARCHAR(45) NULL,
  `DiaReserva` DATE NULL,
  `Paciente_idPaciente` INT NOT NULL,
  PRIMARY KEY (`idReserva`),
  INDEX `fk_Reserva_Paciente1_idx` (`Paciente_idPaciente`),
  CONSTRAINT `fk_Reserva_Paciente1`
    FOREIGN KEY (`Paciente_idPaciente`)
    REFERENCES `mydb`.`Paciente` (`idPaciente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Medico_has_Paciente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Medico_has_Paciente` (
  `Medico_idMedico` INT NOT NULL,
  `Paciente_idPaciente` INT NOT NULL,
  PRIMARY KEY (`Medico_idMedico`, `Paciente_idPaciente`),
  INDEX `fk_Medico_has_Paciente_Paciente1_idx` (`Paciente_idPaciente` ASC) ,
  CONSTRAINT `fk_Medico_has_Paciente_Paciente1`
    FOREIGN KEY (`Paciente_idPaciente`)
    REFERENCES `mydb`.`Paciente` (`idPaciente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Medico_has_Paciente_has_Reserva`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Medico_has_Paciente_has_Reserva` (
  `Medico_has_Paciente_Medico_idMedico` INT NOT NULL,
  `Medico_has_Paciente_Paciente_idPaciente` INT NOT NULL,
  `Reserva_idReserva` INT NOT NULL,
  PRIMARY KEY (`Medico_has_Paciente_Medico_idMedico`, `Medico_has_Paciente_Paciente_idPaciente`, `Reserva_idReserva`),
  INDEX `fk_Medico_has_Paciente_has_Reserva_Reserva1_idx` (`Reserva_idReserva` ASC) ,
  INDEX `fk_Medico_has_Paciente_has_Reserva_Medico_has_Paciente1_idx` (`Medico_has_Paciente_Medico_idMedico` ASC, `Medico_has_Paciente_Paciente_idPaciente` ASC) ,
  CONSTRAINT `fk_Medico_has_Paciente_has_Reserva_Medico_has_Paciente1`
    FOREIGN KEY (`Medico_has_Paciente_Medico_idMedico` , `Medico_has_Paciente_Paciente_idPaciente`)
    REFERENCES `mydb`.`Medico_has_Paciente` (`Medico_idMedico` , `Paciente_idPaciente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Medico_has_Paciente_has_Reserva_Reserva1`
    FOREIGN KEY (`Reserva_idReserva`)
    REFERENCES `mydb`.`Reserva` (`idReserva`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Reserva_has_Paciente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Reserva_has_Paciente` (
  `Reserva_idReserva` INT NOT NULL,
  `Paciente_idPaciente` INT NOT NULL,
  PRIMARY KEY (`Reserva_idReserva`, `Paciente_idPaciente`),
  INDEX `fk_Reserva_has_Paciente_Paciente1_idx` (`Paciente_idPaciente` ASC) ,
  INDEX `fk_Reserva_has_Paciente_Reserva1_idx` (`Reserva_idReserva` ASC) ,
  CONSTRAINT `fk_Reserva_has_Paciente_Reserva1`
    FOREIGN KEY (`Reserva_idReserva`)
    REFERENCES `mydb`.`Reserva` (`idReserva`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reserva_has_Paciente_Paciente1`
    FOREIGN KEY (`Paciente_idPaciente`)
    REFERENCES `mydb`.`Paciente` (`idPaciente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;# EPE3_Claudio_Rocha
EPE3 Taller app . net

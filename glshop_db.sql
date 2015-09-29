/*
SQLyog Community v8.61 
MySQL - 5.6.23-log : Database - glshop_db
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`glshop_db` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `glshop_db`;

/*Table structure for table `album` */

DROP TABLE IF EXISTS `album`;

CREATE TABLE `album` (
  `idalbum` int(11) NOT NULL AUTO_INCREMENT,
  `imealbuma` varchar(45) NOT NULL,
  `zanr` int(11) NOT NULL,
  `cijena` int(11) NOT NULL,
  `izvodac` int(11) NOT NULL,
  `slika` varchar(45) DEFAULT NULL,
  `opis` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`idalbum`),
  KEY `zanr_idx` (`zanr`),
  KEY `FK_album` (`izvodac`),
  CONSTRAINT `FK_album` FOREIGN KEY (`izvodac`) REFERENCES `izvodac` (`idizvodaca`),
  CONSTRAINT `zanr` FOREIGN KEY (`zanr`) REFERENCES `zanr` (`idzanr`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Data for the table `album` */

insert  into `album`(`idalbum`,`imealbuma`,`zanr`,`cijena`,`izvodac`,`slika`,`opis`) values (1,'In Their Darkened Shrines',2,20,1,'In Their Darkened Shrines.jpg','In Their Darkened Shrines is the third studio album by American death metal band Nile. It was released on August 20, 2002 through Relapse Records. The lyrics to \"In Their Darkened Shrines IV: Ruins\" are borrowed from the H. P. Lovecraft short story, \"The Nameless City\". These lyrics are not actually spoken in the song, as it is an instrumental. Along with \"In Their Darkened Shrines I: Hall of Saurian Entombment\", the lyrics are supplementary to the theme of the instrumental. The two main melodies of \"Unas Slayer Of The Gods\" are a tribute to \"Gothic Stone/The Well of Souls\" by Candlemass, from their doom metal album Nightfall.'),(2,'Colored Sands',2,50,2,'Colored Sands.jpg','Colored Sands is the fifth full-length album by technical death metal band Gorguts. It is Gorguts\' first studio album since 2001\'s From Wisdom to Hate. The album features the band\'s first recordings with guitarist Kevin Hufnagel and bassist Colin Marston, and the band\'s only recordings with drummer John Longstreth. It is a concept album based on Tibet. The album was released digitally on August 6, 2013, and the release of the CD and vinyl versions on September 3, 2013.'),(3,'Loveless',3,30,3,'Loveless.jpg','Loveless is the second studio album by Irish shoegaze band My Bloody Valentine. Released on 4 November 1991, Loveless was recorded over a two-year period between 1989 and 1991 in nineteen recording studios. Lead vocalist and guitarist Kevin Shields dominated the recording process; he sought to achieve a particular sound for the record, making use of various techniques such as guitars strummed with a tremolo bar, sampled drum loops, and obscured vocals.'),(4,'Confield',4,30,4,'Confield.jpg','Confield is an album by the electronic music group Autechre, released by Warp Records in 2001. With Confield, Sean Booth and Rob Brown largely abandoned the warm ambient sounds of their earlier works such as Amber and Tri Repetae in favour of the more chaotic feel that they had been pursuing with LP5, EP7, and Peel Session 2.'),(5,'In the Court of the Crimson King',5,20,5,'In the Court of the Crimson King.jpg','In the Court of the Crimson King (subtitled An Observation by King Crimson) is the debut studio album by the British rock group King Crimson, released on 10 October 1969. The album reached number five on the British charts, and is certified gold in the United States, where it reached #28 on the Billboard 200.\r\n'),(6,'Homogenic',6,40,6,'Homogenic.jpg','Homogenic is the fourth studio album by Icelandic musician Björk, released in September 1997. Produced by Björk, Mark Bell, Guy Sigsworth, Howie B and Markus Dravs, it was released on One Little Indian Records. The music of Homogenic was a new style for Björk, focusing on similar sounding music combining electronic beats and string instruments with songs in tribute to her native country Iceland.'),(7,'Spirit of Eden',7,50,7,'Spirit of Eden.jpg','Spirit of Eden is the fourth album by the English band Talk Talk, released in 1988. The album was not critically acclaimed on release, nor did it find commercial success. However it has since gained cult-status and has become highly influential. A number of popular music industry publications have retrospectively named it one of the better albums of the 1980s.');

/*Table structure for table `izvodac` */

DROP TABLE IF EXISTS `izvodac`;

CREATE TABLE `izvodac` (
  `idizvodaca` int(11) NOT NULL AUTO_INCREMENT,
  `nazivizvodaca` varchar(45) NOT NULL,
  PRIMARY KEY (`idizvodaca`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Data for the table `izvodac` */

insert  into `izvodac`(`idizvodaca`,`nazivizvodaca`) values (1,'Nile'),(2,'Gorguts'),(3,'My Bloody Valentine'),(4,'Autechre'),(5,'King Crimson'),(6,'Björk'),(7,'Talk Talk');

/*Table structure for table `korisnik` */

DROP TABLE IF EXISTS `korisnik`;

CREATE TABLE `korisnik` (
  `idkorisnik` int(11) NOT NULL AUTO_INCREMENT,
  `ime` varchar(45) NOT NULL,
  `prezime` varchar(45) NOT NULL,
  `mail` varchar(45) NOT NULL,
  `lozinka` varchar(100) NOT NULL,
  `mjesto` varchar(45) NOT NULL,
  `ulica` varchar(45) NOT NULL,
  `admin` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idkorisnik`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `korisnik` */

insert  into `korisnik`(`idkorisnik`,`ime`,`prezime`,`mail`,`lozinka`,`mjesto`,`ulica`,`admin`) values (1,'Zoran','Puček','a@b.c','aaaa','Varaždin','Varaždinska',1),(2,'Marin','Barišić','c@c.c','cccc','Čakovec','Čakovečka',1),(3,'Stipe','Stipić','b@b.b','bbbb','Stepanovac','Stepska',0),(4,'Darko','Darkić','d@d.d','password64','Daruvar','Daruvarska',0),(5,'Ivana','Ivić','i@i.i','5a5a5a5a','Ivanec','Ivanečka',0);

/*Table structure for table `zanr` */

DROP TABLE IF EXISTS `zanr`;

CREATE TABLE `zanr` (
  `idzanr` int(11) NOT NULL AUTO_INCREMENT,
  `imezanr` varchar(45) NOT NULL,
  PRIMARY KEY (`idzanr`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Data for the table `zanr` */

insert  into `zanr`(`idzanr`,`imezanr`) values (1,'Nedefiniran'),(2,'Technical Death Metal'),(3,'Shoegaze'),(4,'IDM'),(5,'Progressive Rock'),(6,'Art Pop'),(7,'Post-Rock');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

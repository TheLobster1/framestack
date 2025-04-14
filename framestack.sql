-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 14, 2025 at 11:34 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `framestack`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `Id` int(11) NOT NULL,
  `Created` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`Id`, `Created`) VALUES
(1, '2025-04-01 15:10:03'),
(7, '2025-04-09 10:15:41'),
(8, '2025-04-09 13:13:54'),
(9, '2025-04-09 13:25:29'),
(10, '2025-04-09 14:16:25'),
(11, '2025-04-14 23:17:53');

-- --------------------------------------------------------

--
-- Table structure for table `album`
--

CREATE TABLE `album` (
  `Id` int(11) NOT NULL,
  `Name` varchar(24) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `AccountId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `albumpictureconnect`
--

CREATE TABLE `albumpictureconnect` (
  `Id` int(11) NOT NULL,
  `AlbumId` int(11) NOT NULL,
  `PictureId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `familyaccount`
--

CREATE TABLE `familyaccount` (
  `Id` int(11) NOT NULL,
  `Name` varchar(24) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `familyuserconnect`
--

CREATE TABLE `familyuserconnect` (
  `Id` int(11) NOT NULL,
  `FamilyId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `picture`
--

CREATE TABLE `picture` (
  `Id` int(11) NOT NULL,
  `Name` varchar(24) DEFAULT NULL,
  `Description` varchar(120) DEFAULT NULL,
  `File` varchar(255) NOT NULL,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `AccountId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `picture`
--

INSERT INTO `picture` (`Id`, `Name`, `Description`, `File`, `DateCreated`, `AccountId`) VALUES
(47, '20201009_073118.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\2x4n2o1b.jpg', '2025-04-14 21:19:41', 7),
(48, '20201010_105214.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\2kscuvyv.jpg', '2025-04-14 21:19:41', 7),
(49, '20201010_105216.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\0ywbjfit.jpg', '2025-04-14 21:19:41', 7),
(50, '20201010_195649.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\3bbbvadp.jpg', '2025-04-14 21:19:41', 7),
(51, '20201010_195731.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\kkrysb33.jpg', '2025-04-14 21:19:41', 7),
(52, '20201010_195735.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\4q4qtxgr.jpg', '2025-04-14 21:19:41', 7),
(53, '20201010_195738.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\2rwnh5pb.jpg', '2025-04-14 21:19:41', 7),
(54, '20201010_195835.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\31yrlab0.jpg', '2025-04-14 21:19:41', 7),
(55, '20200207_101845_001.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\mjsifv2w.jpg', '2025-04-14 22:58:15', 7),
(56, '20200831_094620.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\2n3ztqtc.jpg', '2025-04-14 22:58:15', 7),
(57, '20200907_125628.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\xtp5rru4.jpg', '2025-04-14 22:58:15', 7),
(58, '20200907_125641.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\o1j3lqwb.jpg', '2025-04-14 22:58:15', 7),
(59, '20201002_143245.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\kbpiiurf.jpg', '2025-04-14 22:58:15', 7),
(60, '20200207_101845_001.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\lg4stat5.jpg', '2025-04-14 22:58:40', 7),
(61, '20200831_094620.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\hzpb1jmw.jpg', '2025-04-14 22:58:40', 7),
(62, '20200907_125628.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\hfya3qho.jpg', '2025-04-14 22:58:40', 7),
(63, '20200907_125641.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\xg21mbey.jpg', '2025-04-14 22:58:40', 7),
(64, '20201002_143245.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\i0vi4irv.jpg', '2025-04-14 22:58:40', 7),
(65, '20201008_082922.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\u2cf3a1u.jpg', '2025-04-14 22:58:40', 7),
(66, '20201009_073118.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\tlpfvomc.jpg', '2025-04-14 22:58:40', 7),
(67, '20201207_201922.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\amp0kxdt.jpg', '2025-04-14 22:59:03', 7),
(68, '20201207_201933.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\doqrp1fb.jpg', '2025-04-14 22:59:03', 7),
(69, '20201207_201946.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\bcxcaq3r.jpg', '2025-04-14 22:59:03', 7),
(70, '20201207_201952.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\gtjsipus.jpg', '2025-04-14 22:59:03', 7),
(71, '20201207_201958.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\q1lrquk5.jpg', '2025-04-14 22:59:03', 7),
(72, '20220223_184128.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\pbfkdisx.jpg', '2025-04-14 23:00:16', 7),
(73, '20220223_184226.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\y3sdd0s4.jpg', '2025-04-14 23:00:16', 7),
(74, '20220223_184357.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\ed5lemwr.jpg', '2025-04-14 23:00:16', 7),
(75, '20220308_132919.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\dhh4v2k5.jpg', '2025-04-14 23:00:16', 7),
(76, '20220327_130128.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\c34qbepn.jpg', '2025-04-14 23:00:16', 7),
(77, '20200207_101845_001.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\zodhfyqp.jpg', '2025-04-14 23:17:03', 7),
(78, '20200831_094620.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\twtsgnam.jpg', '2025-04-14 23:17:03', 7),
(79, '20200907_125628.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\iyqzq2k5.jpg', '2025-04-14 23:17:03', 7),
(80, '20201008_082922.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\znuh412p.jpg', '2025-04-14 23:17:03', 7),
(81, '20201009_073118.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\znpu3axh.jpg', '2025-04-14 23:17:03', 7),
(82, '20201010_105214.jpg', '', 'D:\\FrameStackPictures\\uploads\\rjeveldman1@gmail.com\\qmdhvdkb.jpg', '2025-04-14 23:17:03', 7),
(83, '20200207_101845_001.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\yphmv22r.jpg', '2025-04-14 23:18:07', 11),
(84, '20200831_094620.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\l0hg1z3o.jpg', '2025-04-14 23:18:07', 11),
(85, '20200907_125628.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\h4vcaelb.jpg', '2025-04-14 23:18:07', 11),
(86, '20200907_125641.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\qxzhczlf.jpg', '2025-04-14 23:18:07', 11),
(87, '20201002_143245.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\jo11wrec.jpg', '2025-04-14 23:18:07', 11),
(88, '20210819_165511.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\3xqpb4cf.jpg', '2025-04-14 23:18:18', 11),
(89, '20210819_165735.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\jaa0p12i.jpg', '2025-04-14 23:18:18', 11),
(90, '20210819_165938.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\xd1falul.jpg', '2025-04-14 23:18:18', 11),
(91, '20210820_070652.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\nvyarc2v.jpg', '2025-04-14 23:18:18', 11),
(92, '20210820_105542.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\okmlmwbz.jpg', '2025-04-14 23:18:18', 11),
(93, '20210820_110325.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\polvlgzh.jpg', '2025-04-14 23:18:18', 11),
(94, '20200207_101845_001.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\ju4o3jfw.jpg', '2025-04-14 23:18:27', 11),
(95, '20200831_094620.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\c15vmy1d.jpg', '2025-04-14 23:18:27', 11),
(96, '20200907_125628.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\v0tce43l.jpg', '2025-04-14 23:18:27', 11),
(97, '20200907_125641.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\yhifowcn.jpg', '2025-04-14 23:18:27', 11),
(98, '20201002_143245.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\xybyofjl.jpg', '2025-04-14 23:18:27', 11),
(99, '20200207_101845_001.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\c1z2a04n.jpg', '2025-04-14 23:18:41', 11),
(100, '20200831_094620.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\c0wulywd.jpg', '2025-04-14 23:18:41', 11),
(101, '20200907_125628.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\vnl4cffe.jpg', '2025-04-14 23:18:41', 11),
(102, '20200907_125641.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\epht2c01.jpg', '2025-04-14 23:18:41', 11),
(103, '20201002_143245.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\cslxgnty.jpg', '2025-04-14 23:18:41', 11),
(104, '20201031_184137.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\ylk4jou2.jpg', '2025-04-14 23:18:49', 11),
(105, '20201031_184142.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\hledsduz.jpg', '2025-04-14 23:18:49', 11),
(106, '20201031_184147.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\ickpbc50.jpg', '2025-04-14 23:18:49', 11),
(107, '20201031_184200.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\5z15mtfr.jpg', '2025-04-14 23:18:49', 11),
(108, '20201031_184207.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\bb0bylin.jpg', '2025-04-14 23:18:49', 11),
(109, '20201102_170607.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\beelwppr.jpg', '2025-04-14 23:18:57', 11),
(110, '20201110_151701.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\1ysq42pj.jpg', '2025-04-14 23:18:57', 11),
(111, '20201110_151707.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\skpwrdnp.jpg', '2025-04-14 23:18:57', 11),
(112, '20201110_151713.jpg', '', 'D:\\FrameStackPictures\\uploads\\asdf@asdf.com\\fe1yn2vl.jpg', '2025-04-14 23:18:57', 11);

-- --------------------------------------------------------

--
-- Table structure for table `picturetagconnect`
--

CREATE TABLE `picturetagconnect` (
  `Id` int(11) NOT NULL,
  `TagId` int(11) NOT NULL,
  `PictureId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tag`
--

CREATE TABLE `tag` (
  `Id` int(11) NOT NULL,
  `Name` varchar(24) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tag`
--

INSERT INTO `tag` (`Id`, `Name`) VALUES
(3, 'dogs'),
(1, 'vacation');

-- --------------------------------------------------------

--
-- Table structure for table `useraccount`
--

CREATE TABLE `useraccount` (
  `Id` int(11) NOT NULL,
  `UserName` varchar(120) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Email` varchar(254) NOT NULL,
  `Password` char(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `useraccount`
--

INSERT INTO `useraccount` (`Id`, `UserName`, `FirstName`, `LastName`, `DateOfBirth`, `Email`, `Password`) VALUES
(1, 'RobTEST', 'Rob', 'Veldman', '2003-04-09', 'rob@robdev.nl', '$2a$12$qYxfSaFMomcbplZnLAxjOOcDVMjjAE8Rzz6Ued3xD5HFbe5cvY8kG'),
(7, 'BobWithAnR', 'Rob', 'Veldman', '2003-04-09', 'rjeveldman1@gmail.com', '$2a$13$SgasciPKEdyGIW2mKv.1VeJc50x8lCUQ4o/CtBbtDxGcRaD/RQOjm'),
(8, 'bob', 'bob', 'veldman', '1997-06-30', 'rob@test.com', '$2a$13$zHIqIKkjsMr14l.WEa0ATugBd5baEFk97SL4WekC6Zqv1Mg0AJpke'),
(9, 'testing', 'test', 'test', '2002-06-13', 'mailing@test.com', '$2a$13$xu4NiHHUImMyFVXtHBNinOO0DkMkO698noHSpE1A6DXoUPUqjIrVG'),
(10, 'sander', 'sander', 'achternaam', '2000-06-14', 'sander@mail.com', '$2a$13$FC3uVR4WYVhZnoFbj1Zcc.bK725bqY0xKagUDDW2qYUpTeJOwC7Gm'),
(11, 'asdfuser', 'bobert', 'veldman', '1925-01-01', 'asdf@asdf.com', '$2a$13$stFhXfW/yGk2sIMnTFoi6Okwk8wFWf/0frBsQL3.4bGQZ8u0kmpky');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `album`
--
ALTER TABLE `album`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `AlbumAccountFK` (`AccountId`);

--
-- Indexes for table `albumpictureconnect`
--
ALTER TABLE `albumpictureconnect`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `AlbumConnectFK` (`AlbumId`),
  ADD KEY `PictureConnectFK` (`PictureId`);

--
-- Indexes for table `familyaccount`
--
ALTER TABLE `familyaccount`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `familyuserconnect`
--
ALTER TABLE `familyuserconnect`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FamilyConnectFamilyFK` (`FamilyId`),
  ADD KEY `FamilyConnectUserFK` (`UserId`);

--
-- Indexes for table `picture`
--
ALTER TABLE `picture`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `PictureAccountFK` (`AccountId`);

--
-- Indexes for table `picturetagconnect`
--
ALTER TABLE `picturetagconnect`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `PictureTagPictureConnectFK` (`PictureId`),
  ADD KEY `PictureTagTagConnectFK` (`TagId`);

--
-- Indexes for table `tag`
--
ALTER TABLE `tag`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `TagName` (`Name`);

--
-- Indexes for table `useraccount`
--
ALTER TABLE `useraccount`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Username` (`UserName`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `albumpictureconnect`
--
ALTER TABLE `albumpictureconnect`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `familyuserconnect`
--
ALTER TABLE `familyuserconnect`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `picture`
--
ALTER TABLE `picture`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=113;

--
-- AUTO_INCREMENT for table `picturetagconnect`
--
ALTER TABLE `picturetagconnect`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tag`
--
ALTER TABLE `tag`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `album`
--
ALTER TABLE `album`
  ADD CONSTRAINT `AlbumAccountFK` FOREIGN KEY (`AccountId`) REFERENCES `account` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `albumpictureconnect`
--
ALTER TABLE `albumpictureconnect`
  ADD CONSTRAINT `AlbumConnectFK` FOREIGN KEY (`AlbumId`) REFERENCES `album` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `PictureConnectFK` FOREIGN KEY (`PictureId`) REFERENCES `picture` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `familyaccount`
--
ALTER TABLE `familyaccount`
  ADD CONSTRAINT `FamilyAccountFK` FOREIGN KEY (`Id`) REFERENCES `account` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `familyuserconnect`
--
ALTER TABLE `familyuserconnect`
  ADD CONSTRAINT `FamilyConnectFamilyFK` FOREIGN KEY (`FamilyId`) REFERENCES `familyaccount` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FamilyConnectUserFK` FOREIGN KEY (`UserId`) REFERENCES `useraccount` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `picture`
--
ALTER TABLE `picture`
  ADD CONSTRAINT `PictureAccountFK` FOREIGN KEY (`AccountId`) REFERENCES `account` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `picturetagconnect`
--
ALTER TABLE `picturetagconnect`
  ADD CONSTRAINT `PictureTagPictureConnectFK` FOREIGN KEY (`PictureId`) REFERENCES `picture` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `PictureTagTagConnectFK` FOREIGN KEY (`TagId`) REFERENCES `tag` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `useraccount`
--
ALTER TABLE `useraccount`
  ADD CONSTRAINT `UserAccountFK` FOREIGN KEY (`Id`) REFERENCES `account` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

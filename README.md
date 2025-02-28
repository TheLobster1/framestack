# Framestack
---
## The Idea
### Framestack is a revolutionary new solution to the problem of sharing pictures and videos with the whole family. The project aims at allowing a user to upload a photo or video to the cloud and share it amoung your family. You can group your content however you please whether it be via dedicated albums or tagging, the option is yours.

## Prerequistes
<ol>
  <li>.Net 9</li>
  <li>.Net Maui</li>
  <li> MySQL database server</li>
</ol>


## What's included?
<ul> 
  <li> Windows and Android application </li>
  <li> Account creation </li>
  <li> Cloud storage </li>
  <li> Uploading Photos and videos </li>
  <li> Custom naming and descriptions </li>
  <li> Tagging uploaded content for organisation </li>
  <li> Creating custom tags</li>
  <li> Albums to group your holidays and travels </li>
  <li> Families that people can be a member of </li>
</ul>

## Technical details
This application will use the thread pool to upload multiple files at once.
Async and Await will be used for loading pictures and handling UI navigation.
Locking will be used to prevent resetting various collections throughout the application.
Asynchronous I/O will be used when submitting items to the database. The picture will be uploaded to the server, during which the user can continue to enter details for the picture.

### Class Diagram

![C# threading diagram](https://github.com/TheLobster1/framestack/blob/master/FrameStack.png)

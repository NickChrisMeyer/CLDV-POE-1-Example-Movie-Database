# CLDV6211/w Cloud Development A -- POE

---

## Background

EventEase, an up-and-coming event management company, coordinates a variety of events across different venues. Due to rapid growth and increasing demand, EventEase has been overwhelmed with managing multiple venue bookings, scheduling, and handling booking conflicts. Currently, all scheduling and bookings are managed manually, leading to double bookings, last-minute cancellations, and a lack of visibility into venue availability.

EventEase has contracted your development team to build an efficient, user-friendly online booking platform to streamline these processes. This application should simplify venue management, prevent booking conflicts, provide a clear view of scheduled events, and enable guests to book venues based on availability. The app should also offer search and filtering capabilities to make it easy to view and manage bookings.

The first phase of the project is building the admin platform, so customers will not be able to make bookings themselves; they can email, call, or walk in and request to make a booking, and from there, one of the booking specialists will use the system to make the booking for the customer. An event can be loaded onto the system prior to a venue becoming available, so that once the venue does become available, the booking can be made.

The CEO of EventEase has provided the following requirements that must be present for the new Venue Booking System:

- EventEase needs a user-friendly web application that allows the booking specialists to browse available venues, view events, and make or manage bookings.
- The system should allow authorised personnel to create, view, update, and delete venue and event information, with secure handling of image uploads for each venue.
- Prevent double bookings for venues and restrict deletion of venues or events associated with existing bookings.
- Ensure that all booking information is stored securely (initially locally, eventually in the cloud).
- Some important information to be stored includes the locations, capacity, venue/event names, start & end dates, and unique booking IDs.

EventEase envisions a future where this platform integrates with cloud services, allowing seamless scaling as its needs grow.

---

## Introduction

You will build a Venue Booking System for EventEase using ASP.NET Core MVC. You will adopt a professional "Dev-to-Prod" workflow, starting with a local development environment and culminating in a live cloud deployment.

The application will follow three main development phases, each building upon the previous phase.

### Development Phases

#### Part 1: Project Foundation and Local Development

**Database Design:**

- Develop an Entity-Relationship Diagram (ERD) that includes the Venue, Event, and Booking tables.
- You may add additional tables, but keep the structure simple.

**Initial Application Development:**

- Create an ASP.NET Core MVC project with models for Venues, Events, and Bookings.
- Build the application with the necessary controllers, views, and initial CRUD functionality.
- Database: Configure the application to use SQL LocalDB (or SQL Express) for local data persistence.
- For images, use placeholder URLs (links to sample images) for venues and events in this phase.

**Local Execution:**

- Ensure the web app runs successfully on localhost (IIS Express or Kestrel).
- Ensure data persists in the local database between sessions.

#### Part 2: Enhancing Functionality and Local Cloud Emulation

**Error Handling and Validation:**

- Implement validation rules to prevent double bookings.
- Add logic to restrict the deletion of venues/events associated with active bookings.

**Image Management with Local Blob Emulation:**

- Replace placeholder URLs with image upload functionality.
- Instead of connecting to live Azure Storage, configure the application to use the Azurite Emulator (Local Blob Storage).
- Store images in a local container named `venue-images`.

**Enhanced Display and Search:**

- Develop a consolidated view to display bookings effectively.
- Add a search feature to browse venues, events, and bookings.

#### Part 3: Advanced Filtering and Cloud Migration (Live Deployment)

**Filtering and Event Type Integration:**

- Add a new EventType lookup table.
- Enhance the search functionality to filter results by event type, venue, date range, and availability.

**Go Live - Migration to Azure:**

- This is the "Production" phase. You will migrate your local environment to the live cloud.
- Publish the Web App to Azure App Service.
- Migrate your LocalDB data to a real Azure SQL Database.
- Switch from Azurite to a real Azure Storage Account for images.

**Reflective Report:**

- Submit a report reflecting on the development journey and the specific process of migrating from a local environment to a live cloud environment.

---

## Instructions

This Portfolio of Evidence (POE) requires you to create the Web Application, Database Storage, and Storage components for EventEase, progressively developed through Part 1, Part 2, and culminating in the final POE solution.

To work on the POE, students are required to:

- Install Microsoft Visual Studio 2022 (ensure the "Azure Development" workload is ticked during installation to include the Azurite Emulator).
- Install Azure Storage Explorer (free desktop tool) to manage local blob storage.
- Have access to an Azure account (note: this is strictly required for Part 3 only).

The submission of each part of the POE will require you to do the following:

- Create a document in MS Word which contains the following:
  - Your ST number.
  - The module code.
  - The URL to your GitHub repository source code. If your work is not on GitHub and a link is not added to Arc, you will lose 5% off your hand in.
  - A YouTube video showing full details of your application running and explaining your work. This is needed for any marks to be given.
  - Part 3 Only: The URL of the deployed Web App.
  - Part 3 Only: Screenshots showing that your resources have been deleted.
- All answers required, including typed answers, diagrams, and screenshots.
- The document name must follow the format: `StudentNumber_ModuleCode_Part#`.
- Submit this document in Arc, using the submission link on the Arc page for this module.

---

## Part 1 -- Project Foundation and Initial Deployment (Marks: 100)

*Related Content: Learning Units 1-2*

At the end of this specific part, students should be able to:

**Practically:**

- Design and create a basic database structure.
- Implement ASP.NET Core MVC basics.
- Configure a local SQL Database connection (SQL LocalDB).
- Run a data-driven web application locally.

**Theoretically:**

- Explain the difference between deploying on-premises and in the cloud.
- Identify key differences between Azure hosting models.

### A. Design the EventEase Database

Based on the background information provided earlier, develop an Entity-Relationship Diagram with an accompanying Database Script that will hold the data to be captured into the EventEase system. Your database should contain three (3) main tables: Venue, Event, and Booking.

### B. Develop the Web Application

Build an ASP.NET Core web application with corresponding models, controllers, and views for the EventEase system.

Your web application should include the following:

- The relevant Models, Controllers, and Views for each of the Venues, Events, and Bookings.
- Your application should communicate with SQL LocalDB (or SQL Express), implementing CRUD functionality.
- For images, use placeholder URLs (links to sample images) for venues and events in this phase.

### C. Local Persistence Setup

- Ensure your `appsettings.json` is configured to point to your local database instance.
- Verify that you can stop and start the application without losing data.

### D. Cloud Computing Basics

1. In what ways does deploying an application in the cloud differ from deploying it on-premises, particularly regarding security, deployment speed, and resource management? Use examples to illustrate your points.

2. What are the key differences between Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS)? Why might EventEase benefit from the use of PaaS over the other two when eventually moving to the cloud?

### Submission Notes

- Ensure all the relevant material is present in your submission, i.e., your locally developed web app, ERD, Database Script, Word document, etc.
- Provide screenshots of the application running on localhost.
- Include your GitHub repo; if not submitted to GitHub, it will result in a 5% deduction.
- Provide a detailed YouTube video going over the complete assignment. Your lecturer may not be able to connect to the same database, so ensure the video is detailed and shows everything required in the rubric.

---

## Part 2 -- Enhancing Functionality and Integrating Cloud Storage (Marks: 100)

*Related Content: Learning Units 3-6*

At the end of this specific part, students should be able to:

**Practically:**

- Implement error handling and validation.
- Manage images with Azurite (Local Blob Storage Emulator).
- Enhance application interface and display.
- Implement a basic search functionality.
- Manage configuration for development environments.

**Theoretically:**

- Explain how Azure Cognitive Search differs from traditional search engines.
- Discuss the importance of normalisation in database design.
- Compare the differences between relational and NoSQL databases.

### A. Integrate Local Blob Storage (Azurite)

In Part 1, your event and venue images were being saved as simple URLs. For Part 2, images must be stored using the Azurite Emulator to simulate Azure Blob Storage without using cloud credits.

- Update your `appsettings.json` to use the development storage connection string: `UseDevelopmentStorage=true`.
- Implement code to upload images to a container named `venue-images`.
- Use Azure Storage Explorer to verify the images are being saved locally.

### B. Implementing Error Handling and Validation

Improve the user experience by implementing validation rules and proper error handling.

- Add validation to prevent the double booking of a venue for the same date/time.
- Add validation to restrict the deletion of venues or events associated with active bookings.
- Ensure the application does not crash on common user error actions.
- Display an alert to the user when a validation error occurs.

### C. Enhanced Display and Search

In Part 1, your database included only 3 tables. Now, to enhance the booking display, you are required to incorporate a view that consolidates the relevant booking information.

- This view should include all the relevant information from the venue and event tables.
- Add a simple search function to this new bookings view so the booking specialists can search for bookings via the BookingID or Event Name.

### D. Deployment Verification (Local)

- Submit screenshots of Azure Storage Explorer showing your local `venue-images` container and the uploaded files.
- Submit screenshots of the application displaying these uploaded images.

### E. Database Design and Cognitive Search

- Explain how Azure Cognitive Search differs from traditional search engines and discuss potential use cases where Cognitive Search would offer a clear advantage.
- Why is database normalisation important? Discuss the impact of both normalised and denormalised structures on performance and scalability.

### Submission Notes

- Ensure all the relevant material is present in your submission.
- Include screenshots of Azure Storage Explorer verifying local blob storage.
- This submission must include code attribution and traditional referencing as it includes both practical and theoretical content.
- Provide screenshots of the application running on localhost.
- Include your GitHub repo; if not submitted to GitHub, it will result in a 5% deduction.
- Provide a detailed YouTube video going over the complete assignment.

---

## Part 3 -- Advanced Filtering, Reporting, and Documentation (Marks: 100)

*Related Content: Learning Units 7-9*

At the end of this specific part, students should be able to:

**Practically:**

- Enhance search with filtering and event type classification.
- Migrate local resources to live Azure Cloud resources.
- Deploy an ASP.NET Core Web App to Azure App Service.
- Finalise documentation and reflection.

**Theoretically:**

- Discuss how Cosmos DB differs from traditional databases.
- Discuss key considerations when designing logic apps that handle sensitive data.
- Explain how combining Event Grid with other services can create robust workflows.

### A. Advanced Filtering

Extend the search feature implemented in Part 2:

- Add a new EventType lookup to your database with predefined categories.
- Implement filters in your search functionality to filter results by event type, date range, and venue availability.

### B. The "Go Live" Migration

Move your application from your local environment to the live Azure Cloud.

- **Database:** Create an Azure SQL Database. Migrate your schema and data from your LocalDB to Azure SQL.
- **Storage:** Create an Azure Storage Account. Update your application configuration to point to the live storage keys instead of the local emulator.
- **Web App:** Create an Azure App Service and publish your application.
- **Configuration:** Ensure your deployed app is reading from the live database and live storage, not your local machine.
- Drop all resources and show proof.

### C. Reflective Technical Report

Write a reflective report documenting your development journey, with a specific focus on the transition from Part 2 (Local) to Part 3 (Cloud).

Your report must include:

- A detailed description of the application's full feature list.
- **Component Discussion:** Which Azure services were used and why?
- **The Migration Experience:** Reflect on the process of changing from LocalDB/Azurite to Azure SQL/Storage. What configuration changes were required? Why is this separation of environments important in professional development?
- Discuss the technologies used to build the project and why they were used.

### Submission Notes

- Ensure all the relevant material is present in your submission.
- Provide any relevant URLs, such as the web app URL.
- This submission must include code attribution and traditional referencing.
- Provide screenshots of the application running on the published URL.
- Include your GitHub repo; if not submitted to GitHub, it will result in a 5% deduction.
- Provide a detailed YouTube video going over the complete assignment.
- Show proof that all resources have been dropped.

---

**TOTAL MARKS: 300**
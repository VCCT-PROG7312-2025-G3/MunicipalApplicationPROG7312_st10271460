# MunicipalApplicationPROG7312
## PROG7312 Portfolio of Evidence – Part 1, 2 & 3 Submission

This Windows Forms municipal reporting system was developed as part of the PROG7312 POE. The application enables residents to submit municipal service reports, attach media, adjust accessibility preferences, and track issue statuses using an advanced, data-structure-driven backend.

The project demonstrates strong C# proficiency, UI/UX awareness, and extensive algorithmic understanding, including custom implementations of:

Linked Lists

Stacks

Queues

Dictionaries / Hash Lookups

Binary Trees

AVL Trees

Heaps (MinHeap)

Graphs + Prim’s Minimum Spanning Tree

SortedDictionary (Red-Black Tree)

# 🚀 Application Features
# 🔹 Part 1 — Report Issue Form

A fully interactive form where users can submit municipal service complaints.

Core Features

Capture Location, Category, and Description

Attach and remove multiple media files

Mandatory POPIA Consent checkbox

Live validation using ErrorProvider

Progress Bar updates with each completed required field

Accessibility features:

Adjustable font size

Light/Dark mode

### New Enhancements Based on Lecturer Feedback

✔ Submission Summary Popup
Users see a generated summary of exactly what they submitted.

✔ User Rating System
After submission, the app allows users to rate the tool (1–5).
Rating is stored for future analytics (or can be extended).

✔ Global settings broadcaster
Form text updates instantly using runtime resource reapplication.

# 📊 Part 2 — Issue Tracking & Status Form

This module showcases the use of multiple interactive data structures, all dynamically synced to the UI.

Key Functions

View all reported issues

Live search by Ticket ID in O(1) time

Track status transitions (Pending → In Progress → Resolved)

Updates all data structures in real-time

### Data Structures Used:
Structure	Purpose	Example Implementation
Dictionary<Guid, Issue>	Fast O(1) issue lookup	_index
Queue	FIFO pending workflow	_pending
Stack	Recently viewed issues	_recentLookups
LinkedList	In-memory ordered storage	IssueStore.Instance.All()
LinkedList (attachments)	File paths storage	Issue.Attachments
Additional Enhancements

Auto-refresh grid

Live counters

Error handling for missing IDs

Modular UI/Domain/Persistence layers

# 🧠 Part 3 — Service Request Status (Advanced DS Project)

Part 3 demonstrates mastery of advanced data structures and algorithms by implementing a smart municipal job-scheduling system.

What This Screen Does

Loads all service requests into optimised structures

Allows priority-driven processing

Provides category recommendations

Performs timestamp indexing

Computes fieldworker routing via MST

Visualises queue and priority behaviour dynamically

# 🧩 Data Structures in Part 3
## 1️⃣ MinHeap — Priority Processing

Used when clicking Process Next (Priority).

Highest priority = smallest numeric value

Tie-breaker: earliest submitted request

Ensures realistic municipal workflow handling

## 2️⃣ Undo System — Stack

Every processed request is pushed into an undo stack

Provides LIFO rollback

Demonstrates command reversal

## 3️⃣ Category Tree — Binary Tree

Built from all service categories

In-order traversal sorted recommendation list

Shows non-framework tree construction & traversal

## 4️⃣ Timestamp Index — AVL Tree

Key: SubmittedAt.Ticks

Fast lookup and balanced structure

Demonstrates rotations & O(log n) performance

## 5️⃣ SortedDictionary — Date Range Filtering

Red-black tree implementation

Efficient range-scanning for requests within specific dates

## 6️⃣ Graph + Prim’s Minimum Spanning Tree (MST)

Activated via Compute MST button.

Nodes represent requests + depot

Edges weighted by distance and priority

Produces optimal route for municipal technicians

Demonstrates practical real-world graph usage

## 🎨 Design & Accessibility Enhancements

Light/Dark theme toggle

Adjustable font sizes for readability

Dynamic refresh when changing UI settings

## 🛠️ Installation & Setup

Clone or download the repository

Open the solution in Visual Studio 2022

Build using .NET 8.0 (Windows)

Run using Ctrl + F5

No external database is required — everything runs in memory.

# 📖 Usage Summary
## Part 1 — Report Issue

Select Report Issue from Main Menu

Complete required fields

Attach media

Tick POPIA consent

Watch progress bar update

Press Submit

See:

Summary popup

1–5 star rating

Success message (As per lecturers feedback)

## Part 2 — Issue Status

Browse issues

Search for Ticket IDs

Watch queues/stacks update live

Observe pending and recent trackers

## Part 3 — Service Request Status

Click Process Next → heap-driven priority execution

Click Undo → stack-based reversal

Change filters → SortedDictionary-driven

Inspect recommendations → Binary tree traversal

Compute MST → Graph + Prim’s algorithm

## 👨‍💻 Developer

Name: Reuven-Jon Kadalie
Student Number: ST10271460
Module: PROG7312

## ⚖️ Notes

Project intentionally uses no database

Data is stored in-memory for demonstration purposes

Full demonstration video included (Word doc Submission)

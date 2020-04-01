﻿// <copyright file="MeasureGenerator.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
//     Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using generator_cli.Models;
using Hl7.Fhir.Model;
using static generator_cli.Generators.CommonLiterals;

namespace generator_cli.Generators
{
    /// <summary>A measure generator.</summary>
    public static class MeasureGenerator
    {
        /// <summary>The publication date.</summary>
        private const string PublicationDate = "2020-03-31T00:00:00Z";

        /// <summary>The publisher.</summary>
        private const string Publisher = "SANER-IG";

        /// <summary>The canonical URL base.</summary>
        public const string CDCCanonicalUrl = "http://cdcmeasures.example.org/modules/covid19/20200331";

        /// <summary>The canonical URL base.</summary>
        public const string CanonicalUrl = "http://saner.example.org/covid19/20200331";

        /// <summary>The identifier screening rate.</summary>
        public const string IdScreeningRate = "screening-rate";

        /// <summary>Number of identifier tests.</summary>
        public const string IdTestCount = "covid-19-test-count";

        /// <summary>Number of identifier test positives.</summary>
        public const string IdTestPositiveCount = "covid-19-test-positive-count";

        /// <summary>The identifier recovered.</summary>
        public const string IdRecovered = "covid-19-recovered";

        /// <summary>The CDC total beds.</summary>
        public const string CDCTotalBeds = "numTotBeds";

        /// <summary>The CDC inpatient beds.</summary>
        public const string CDCInpatientBeds = "numbeds";

        /// <summary>The CDC inpatient bed occupancy.</summary>
        public const string CDCInpatientBedOccupancy = "numBedsOcc";

        /// <summary>The CDC icu beds.</summary>
        public const string CDCIcuBeds = "numICUBeds";

        /// <summary>The CDC icu bed occupancy.</summary>
        public const string CDCIcuBedOccupancy = "numICUBedsOcc";

        /// <summary>The CDC ventilators.</summary>
        public const string CDCVentilators = "numVent";

        /// <summary>The cdc ventilators in use.</summary>
        public const string CDCVentilatorsInUse = "numVentUse";

        /// <summary>The cdc hospitalized patients.</summary>
        public const string CDCHospitalizedPatients = "numC19HospPats";

        /// <summary>The cdc ventilated patients.</summary>
        public const string CDCVentilatedPatients = "numC19MechVentPats";

        /// <summary>The cdc hospital onset.</summary>
        public const string CDCHospitalOnset = "numC19HOPats";

        /// <summary>The cdc overflow patients.</summary>
        public const string CDCAwaitingBeds = "numC19OverflowPats";

        /// <summary>The cdc awaiting ventilators.</summary>
        public const string CDCAwaitingVentilators = "numC19OFMechVentPats";

        /// <summary>The cdc died.</summary>
        public const string CDCDied = "numC19Died";

        /// <summary>Gets the screening rate.</summary>
        /// <value>The screening rate.</value>
        public static Measure ScreeningRate => new Measure()
        {
            Id = IdScreeningRate,
            Name = IdScreeningRate,
            Url = $"{CDCCanonicalUrl}/{IdScreeningRate}",
            Title = "Screening Rate",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown("Percentage of patients with a face-to-face, telehealth, telephone, or admission encounter for whom a COVID-19 communicable disease screening was performed."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
            Group = new List<Measure.GroupComponent>()
            {
                new Measure.GroupComponent()
                {
                    Population = new List<Measure.PopulationComponent>()
                    {
                        new Measure.PopulationComponent()
                        {
                            Code = FhirTriplet.Numerator.Concept,
                            Description = "Patients for whom a COVID-19 communicable disease screening was performed.",
                        },
                        new Measure.PopulationComponent()
                        {
                            Code = FhirTriplet.Denominator.Concept,
                            Description = "Patients with a face-to-face, telehealth, telephone, or admission encounter.",
                        },
                    },
                },
            },
        };

        /// <summary>Gets the test total.</summary>
        /// <value>The test total.</value>
        public static Measure TestTotal => new Measure()
        {
            Id = IdTestCount,
            Name = IdTestCount,
            Url = $"{CanonicalUrl}/{IdTestCount}",
            Title = "COVID-19 Tests Performed",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown("The total number of patients for whom a test for COVID-19 was ordered."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the test positive total.</summary>
        /// <value>The test positive total.</value>
        public static Measure TestPositiveTotal => new Measure()
        {
            Id = IdTestPositiveCount,
            Name = IdTestPositiveCount,
            Url = $"{CanonicalUrl}/{IdTestPositiveCount}",
            Title = "COVID-19 Positive Tests",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "The total number of patients for whom a positive result for a COVID-19 " +
                "test was documented."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the beds total.</summary>
        /// <value>The beds total.</value>
        public static Measure BedsTotal => new Measure()
        {
            Id = CDCTotalBeds,
            Name = CDCTotalBeds,
            Url = $"{CDCCanonicalUrl}/{CDCTotalBeds}",
            Title = "All Hospital Beds",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Total number of all Inpatient and outpatient beds, " +
                "including all staffed, ICU, licensed, and overflow(surge) beds used for " +
                "inpatients or outpatients."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the inpatient beds total.</summary>
        /// <value>The inpatient beds total.</value>
        public static Measure InpatientBedsTotal => new Measure()
        {
            Id = CDCInpatientBeds,
            Name = CDCInpatientBeds,
            Url = $"{CDCCanonicalUrl}/{CDCInpatientBeds}",
            Title = "Hospital Inpatient Beds",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Inpatient beds, including all staffed, licensed, " +
                "and overflow(surge) beds used for inpatients."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the inpatient beds occupied.</summary>
        /// <value>The inpatient beds occupied.</value>
        public static Measure InpatientBedsOccupied => new Measure()
        {
            Id = CDCInpatientBedOccupancy,
            Name = CDCInpatientBedOccupancy,
            Url = $"{CDCCanonicalUrl}/{CDCInpatientBedOccupancy}",
            Title = "Hospital Inpatient Bed Occupancy",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Total number of staffed inpatient " +
                "beds that are occupied."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the icu beds total.</summary>
        /// <value>The icu beds total.</value>
        public static Measure IcuBedsTotal => new Measure()
        {
            Id = CDCIcuBeds,
            Name = CDCIcuBeds,
            Url = $"{CDCCanonicalUrl}/{CDCIcuBeds}",
            Title = "Hospital ICU Beds",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Total number of staffed inpatient intensive care unit (ICU) beds."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the icu beds occupied.</summary>
        /// <value>The icu beds occupied.</value>
        public static Measure IcuBedsOccupied => new Measure()
        {
            Id = CDCIcuBedOccupancy,
            Name = CDCIcuBedOccupancy,
            Url = $"{CDCCanonicalUrl}/{CDCIcuBedOccupancy}",
            Title = "Hospital ICU Bed Occupancy",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Total number of staffed inpatient ICU beds that are occupied."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the ventilators total.</summary>
        /// <value>The ventilators total.</value>
        public static Measure VentilatorsTotal => new Measure()
        {
            Id = CDCVentilators,
            Name = CDCVentilators,
            Url = $"{CDCCanonicalUrl}/{CDCVentilators}",
            Title = "Mechanical Ventilators",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Total number of ventilators available."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the ventilators in use.</summary>
        /// <value>The ventilators in use.</value>
        public static Measure VentilatorsInUse => new Measure()
        {
            Id = CDCVentilatorsInUse,
            Name = CDCVentilatorsInUse,
            Url = $"{CDCCanonicalUrl}/{CDCVentilatorsInUse}",
            Title = "Mechanical Ventilators In Use",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Total number of ventilators in use."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the COVID patients hospitalized.</summary>
        /// <value>The COVID patients hospitalized.</value>
        public static Measure CovidPatientsHospitalized => new Measure()
        {
            Id = CDCHospitalizedPatients,
            Name = CDCHospitalizedPatients,
            Url = $"{CDCCanonicalUrl}/{CDCHospitalizedPatients}",
            Title = "COVID-19 Patients Hospitalized",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients currently hospitalized in an inpatient care location " +
                "who have suspected or confirmed COVID-19."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the covid patients ventilated.</summary>
        /// <value>The covid patients ventilated.</value>
        public static Measure CovidPatientsVentilated => new Measure()
        {
            Id = CDCVentilatedPatients,
            Name = CDCVentilatedPatients,
            Url = $"{CDCCanonicalUrl}/{CDCVentilatedPatients}",
            Title = "COVID-19 Patients Hospitalized and Ventilated",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients hospitalized in an NHSN inpatient " +
                "care location who have suspected or confirmed COVID - 19 and are on a " +
                "mechanical ventilator."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the covid hospital onset.</summary>
        /// <value>The covid hospital onset.</value>
        public static Measure CovidHospitalOnset => new Measure()
        {
            Id = CDCHospitalOnset,
            Name = CDCHospitalOnset,
            Url = $"{CDCCanonicalUrl}/{CDCHospitalOnset}",
            Title = "COVID-19 Hospital Onset",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients hospitalized in an NHSN inpatient care location " +
                "with onset of suspected or confirmed COVID - 19 14 or more days after " +
                "hospitalization."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the covid awaiting inpatient.</summary>
        /// <value>The covid awaiting inpatient.</value>
        public static Measure CovidAwaitingBed => new Measure()
        {
            Id = CDCAwaitingBeds,
            Name = CDCAwaitingBeds,
            Url = $"{CDCCanonicalUrl}/{CDCAwaitingBeds}",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients with suspected or confirmed COVID-19 who are in " +
                "the ED or any overflow location awaiting an inpatient bed."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the covid awaiting ventilator.</summary>
        /// <value>The covid awaiting ventilator.</value>
        public static Measure CovidAwaitingVentilator => new Measure()
        {
            Id = CDCAwaitingVentilators,
            Name = CDCAwaitingVentilators,
            Url = $"{CDCCanonicalUrl}/{CDCAwaitingVentilators}",
            Title = "COVID-19 Patients Awaiting Ventilators",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients with suspected or confirmed " +
                "COVID - 19 who are in the ED or any overflow location awaiting an inpatient " +
                "bed and on a mechanical ventilator."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the covid recovered.</summary>
        /// <value>The covid recovered.</value>
        public static Measure CovidRecovered => new Measure()
        {
            Id = IdRecovered,
            Name = IdRecovered,
            Url = $"{CanonicalUrl}/{IdRecovered}",
            Title = "COVID-19 Patients Recovered",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients with suspected or confirmed COVID-19 who have recovered " +
                "and been discharged."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets the covid died.</summary>
        /// <value>The covid died.</value>
        public static Measure CovidDied => new Measure()
        {
            Id = CDCDied,
            Name = CDCDied,
            Url = $"{CDCCanonicalUrl}/{CDCDied}",
            Title = "COVID-19 Patients Died",
            Status = PublicationStatus.Draft,
            Date = PublicationDate,
            Publisher = Publisher,
            Description = new Markdown(
                "Patients with suspected or confirmed COVID-19 who died in the " +
                "hospital, ED, or any overflow location."),
            UseContext = new List<UsageContext>()
            {
                new UsageContext()
                {
                    Code = FhirTriplet.GetCode(SystemLiterals.UsageContextType, ContextFocus),
                    Value = FhirTriplet.SctCovid.Concept,
                },
            },
        };

        /// <summary>Gets report bundle.</summary>
        /// <returns>The report bundle.</returns>
        public static Bundle GetMeasureBundle()
        {
            string bundleId = FhirGenerator.NextId;

            Bundle bundle = new Bundle()
            {
                Id = bundleId,
                Identifier = FhirGenerator.IdentifierForId(bundleId),
                Type = Bundle.BundleType.Collection,
                Timestamp = new DateTimeOffset(DateTime.Now),
                Meta = new Meta(),
            };

            bundle.Entry = new List<Bundle.EntryComponent>();

            bundle.AddResourceEntry(
                TestTotal,
                $"{SystemLiterals.Internal}Measure/{TestTotal.Id}");

            bundle.AddResourceEntry(
                TestPositiveTotal,
                $"{SystemLiterals.Internal}Measure/{TestPositiveTotal.Id}");

            bundle.AddResourceEntry(
                BedsTotal,
                $"{SystemLiterals.Internal}Measure/{BedsTotal.Id}");

            bundle.AddResourceEntry(
                InpatientBedsTotal,
                $"{SystemLiterals.Internal}Measure/{InpatientBedsTotal.Id}");

            bundle.AddResourceEntry(
                InpatientBedsOccupied,
                $"{SystemLiterals.Internal}Measure/{InpatientBedsOccupied.Id}");

            bundle.AddResourceEntry(
                IcuBedsTotal,
                $"{SystemLiterals.Internal}Measure/{IcuBedsTotal.Id}");

            bundle.AddResourceEntry(
                IcuBedsOccupied,
                $"{SystemLiterals.Internal}Measure/{IcuBedsOccupied.Id}");

            bundle.AddResourceEntry(
                VentilatorsTotal,
                $"{SystemLiterals.Internal}Measure/{VentilatorsTotal.Id}");

            bundle.AddResourceEntry(
                VentilatorsInUse,
                $"{SystemLiterals.Internal}Measure/{VentilatorsInUse.Id}");

            bundle.AddResourceEntry(
                CovidPatientsHospitalized,
                $"{SystemLiterals.Internal}Measure/{CovidPatientsHospitalized.Id}");

            bundle.AddResourceEntry(
                CovidPatientsVentilated,
                $"{SystemLiterals.Internal}Measure/{CovidPatientsVentilated.Id}");

            bundle.AddResourceEntry(
                CovidHospitalOnset,
                $"{SystemLiterals.Internal}Measure/{CovidHospitalOnset.Id}");

            bundle.AddResourceEntry(
                CovidAwaitingBed,
                $"{SystemLiterals.Internal}Measure/{CovidAwaitingBed.Id}");

            bundle.AddResourceEntry(
                CovidAwaitingVentilator,
                $"{SystemLiterals.Internal}Measure/{CovidAwaitingVentilator.Id}");

            bundle.AddResourceEntry(
                CovidRecovered,
                $"{SystemLiterals.Internal}Measure/{CovidRecovered.Id}");

            bundle.AddResourceEntry(
                CovidDied,
                $"{SystemLiterals.Internal}Measure/{CovidDied.Id}");

            return bundle;
        }
    }
}
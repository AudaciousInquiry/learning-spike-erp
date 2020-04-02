﻿// <copyright file="MeasureReportGenerator.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
//     Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using generator_cli.Models;
using Hl7.Fhir.Model;

namespace generator_cli.Generators
{
    /// <summary>A measure report generator.</summary>
    public class MeasureReportGenerator
    {
        private Organization _org;
        private Location _location;
        private OrgDeviceData _deviceData;
        private OrgPatientData _patientData;
        private OrgTestData _testData;
        private Period _period;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasureReportGenerator"/> class.
        /// </summary>
        /// <param name="org">        The organisation.</param>
        /// <param name="location">   The location.</param>
        /// <param name="deviceData"> Information describing the device.</param>
        /// <param name="patientData">Information describing the patient.</param>
        /// <param name="testData">   Information describing the test.</param>
        /// <param name="period">     The period.</param>
        public MeasureReportGenerator(
            Organization org,
            Location location,
            OrgDeviceData deviceData,
            OrgPatientData patientData,
            OrgTestData testData,
            Period period)
        {
            if (org == null)
            {
                throw new ArgumentNullException(nameof(org));
            }

            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (deviceData == null)
            {
                throw new ArgumentNullException(nameof(deviceData));
            }

            if (patientData == null)
            {
                throw new ArgumentNullException(nameof(patientData));
            }

            if (testData == null)
            {
                throw new ArgumentNullException(nameof(testData));
            }

            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }

            _org = org;
            _location = location;
            _deviceData = deviceData;
            _patientData = patientData;
            _testData = testData;
            _period = period;
        }

        /// <summary>Gets report bundle.</summary>
        /// <returns>The report bundle.</returns>
        public Bundle GetReportBundle()
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

            string id;
            int val;

            bundle.Entry = new List<Bundle.EntryComponent>();

            // bundle.AddResourceEntry(
            //     ReportForMeasure(out id, MeasureGenerator.TestTotal, _testData.Performed),
            //     $"{SystemLiterals.Internal}MeasureReport/{id}");
            // bundle.AddResourceEntry(
            //     ReportForMeasure(out id, MeasureGenerator.TestPositiveTotal, _testData.Positive),
            //     $"{SystemLiterals.Internal}MeasureReport/{id}");

            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCTotalBeds),
                    _deviceData.TotalBeds),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCInpatientBeds),
                    _deviceData.Inpatient),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Min(_deviceData.Inpatient, _patientData.Total);
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCInpatientBedOccupancy),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCIcuBeds),
                    _deviceData.ICU),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Min(_deviceData.ICU, _patientData.NegativeNeedIcu + _patientData.PositiveNeedIcu);
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCIcuBedOccupancy),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCVentilators),
                    _deviceData.Ventilators),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Min(_deviceData.Ventilators, _patientData.NegativeNeedVent + _patientData.PositiveNeedVent);
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCVentilatorsInUse),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Min(_deviceData.TotalBeds - _patientData.Negative, _patientData.Positive);
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCHospitalizedPatients),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Min(_deviceData.Ventilators - _patientData.NegativeNeedVent, _patientData.PositiveNeedVent);
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCVentilatedPatients),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCHospitalOnset),
                    _patientData.OnsetInCare),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Max(0, _patientData.Positive - (_deviceData.TotalBeds - _patientData.Negative));
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCAwaitingBeds),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            val = Math.Max(0, _patientData.PositiveNeedVent - (_deviceData.Ventilators - _patientData.NegativeNeedVent));
            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCAwaitingVentilators),
                    val),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            bundle.AddResourceEntry(
                ReportForMeasure(
                    out id,
                    MeasureGenerator.CDCMeasure(MeasureGenerator.CDCDied),
                    _patientData.Died),
                $"{SystemLiterals.Internal}MeasureReport/{id}");

            return bundle;
        }

        /// <summary>Reports for measure.</summary>
        /// <param name="id">         [out] The identifier.</param>
        /// <param name="measure">    The measure.</param>
        /// <param name="score">      The score.</param>
        /// <param name="numerator">  (Optional) The numerator.</param>
        /// <param name="denominator">(Optional) The denominator.</param>
        /// <returns>A MeasureReport.</returns>
        private MeasureReport ReportForMeasure(
            out string id,
            Measure measure,
            decimal score,
            int? numerator = null,
            int? denominator = null)
        {
            id = FhirGenerator.NextId;

            MeasureReport report = new MeasureReport()
            {
                Id = id,
                Subject = new ResourceReference(
                    $"{_location.ResourceType}/{_location.Id}",
                    _org.Name),
                Status = MeasureReport.MeasureReportStatus.Complete,
                Type = MeasureReport.MeasureReportType.Summary,
                Date = new FhirDateTime(new DateTimeOffset(DateTime.Now)).ToString(),
                Period = _period,
                Measure = measure.Url,
                Reporter = new ResourceReference(
                    $"{_org.ResourceType}/{_org.Id}",
                    _org.Name),
                Group = new List<MeasureReport.GroupComponent>()
                {
                    new MeasureReport.GroupComponent(),
                },
            };

            report.Group[0].MeasureScore = new Quantity() { Value = score };
            report.Group[0].Population = new List<MeasureReport.PopulationComponent>();

            if (measure.Group[0].Population != null)
            {
                foreach (Measure.PopulationComponent population in measure.Group[0].Population)
                {
                    if ((population.Code == null) ||
                        (population.Code.Coding == null) ||
                        (population.Code.Coding.Count == 0))
                    {
                        continue;
                    }

                    switch (population.Code.Coding[0].Code)
                    {
                        case "measure-population":
                            report.Group[0].Population.Add(new MeasureReport.PopulationComponent()
                            {
                                Code = population.Code,
                                Count = (int)score,
                            });
                            break;

                        case "measure-observation":
                            // ignore
                            break;

                        case "numerator":
                            report.Group[0].Population.Add(new MeasureReport.PopulationComponent()
                            {
                                Code = population.Code,
                                Count = numerator ?? 1,
                            });
                            break;

                        case "denominator":
                            report.Group[0].Population.Add(new MeasureReport.PopulationComponent()
                            {
                                Code = population.Code,
                                Count = denominator ?? 1,
                            });
                            break;
                    }
                }
            }

            if (report.Group[0].Population.Count == 0)
            {
                report.Group[0].Population = null;
            }

            return report;
        }
    }
}

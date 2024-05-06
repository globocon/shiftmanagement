var Year3, Year2, Year1;
var Year3g, Year2g, Year1g;
var Year3rc, Year2rc, Year1rc;


$(function () {
	"use strict";
	$.ajax({
		url: '/Index/?handler=GroupByYears',
		type: 'GET',
		dataType: 'json',
	}).done(function (response) {

		console.log('Data loaded successfully:');
		if (Year3 === null || Year3 === undefined) {
			console.log('Year3 is either null or undefined');
		} else {
			console.log('Year3 is not null or undefined');
		}

		if (response.value[0].year === null || response.value[0].year === undefined){
		Year3 = new Date().getFullYear();
		}else {
			Year3 = response.value[0].year;
			Year3g = response.value[0].count
			Year3rc = response.value[0].respondCount
		}
		if (response.value[1].year === null || response.value[1].year === undefined) {
			Year2 = Year3-1
		} else {
			Year2 = response.value[1].year;
			Year2g = response.value[1].count
			Year2rc = response.value[1].respondCount
		}
		if (response.value[2] === null || response.value[2]=== undefined) {
			Year1 = Year2 - 1
			Year1g = 0
			Year1rc = 0
		} else {
			Year1 = response.value[2].year;
			Year1g = response.value[2].count
			Year1rc = response.value[2].respondCount

		}
		//Year2 = response.value[1];
		//Year1 = response.value[2];

		//$.ajax({
		//	url: '/Index/?handler=GroupByYears',
		//	type: 'GET',
		//	dataType: 'json',
		//}).done(function (response) {
		//	Year3g = response.value[0];
		//	Year2g = response.value[1];
		//	Year1g = response.value[2];

		
	// chart yr1-Last 3 year requests
	var options = {
		chart: {
			height: 150,
			type: 'radialBar',
			toolbar: {
				show: false
			}
		},
		plotOptions: {
			radialBar: {
				//startAngle: -135,
				//endAngle: 225,
				hollow: {
					margin: 0,
					size: '78%',
					background: '#ceffca',
					image: undefined,
					imageOffsetX: 0,
					imageOffsetY: 0,
					position: 'front',
					dropShadow: {
						enabled: false,
						top: 3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				track: {
					// background: '#f8f9fa',
					//strokeWidth: '67%',
					margin: 0, // margin is in pixels
					dropShadow: {
						enabled: false,
						top: -3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				dataLabels: {
					showOn: 'always',
					name: {
						offsetY: -8,
						show: true,
						color: '#6c757d',
						fontSize: '15px'
					},
					value: {
						formatter: function (val) {
							return val;
						},
						color: '#000',
						fontSize: '25px',
						show: true,
						offsetY: 10,
					}
				}
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				shade: 'light',
				type: 'horizontal',
				shadeIntensity: 0.5,
				gradientToColors: ['#17a00e'],
				inverseColors: false,
				opacityFrom: 1,
				opacityTo: 1,
				stops: [0, 100]
			}
		},
		colors: ["#17a00e"],
		//series: [240],
		series: ['' + Year1g],
		stroke: {
			lineCap: 'round',
			width: '5',
			//dashArray: 4
		},
		labels: [''+Year1],
	}
	var chart = new ApexCharts(document.querySelector("#yr1"), options);
	chart.render();
	// chart yr2
	var options = {
		chart: {
			height: 150,
			type: 'radialBar',
			toolbar: {
				show: false
			}
		},
		plotOptions: {
			radialBar: {
				//startAngle: -135,
				//endAngle: 225,
				hollow: {
					margin: 0,
					size: '78%',
					background: '#ffd6da',
					image: undefined,
					imageOffsetX: 0,
					imageOffsetY: 0,
					position: 'front',
					dropShadow: {
						enabled: false,
						top: 3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				track: {
					// background: '#f8f9fa',
					//strokeWidth: '67%',
					margin: 0, // margin is in pixels
					dropShadow: {
						enabled: false,
						top: -3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				dataLabels: {
					showOn: 'always',
					name: {
						offsetY: -8,
						show: true,
						color: '#6c757d',
						fontSize: '15px'
					},
					value: {
						formatter: function (val) {
							return val;
						},
						color: '#000',
						fontSize: '25px',
						show: true,
						offsetY: 10,
					}
				}
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				shade: 'light',
				type: 'horizontal',
				shadeIntensity: 0.5,
				gradientToColors: ['#f41127'],
				inverseColors: false,
				opacityFrom: 1,
				opacityTo: 1,
				stops: [0, 100]
			}
		},
		colors: ["#f41127"],
		//series: [160],
		series: ['' + Year2g],
		stroke: {
			lineCap: 'round',
			//dashArray: 4
		},
		labels: [""+Year2],
	}
	var chart = new ApexCharts(document.querySelector("#yr2"), options);
	chart.render();
	// chart yr3
	var options = {
			chart: {
				height: 180,
				type: 'radialBar',
				toolbar: {
					show: false
				}
			},
			plotOptions: {
				radialBar: {
					//startAngle: -135,
					//endAngle: 225,
					hollow: {
						margin: 0,
						size: '78%',
						background: '#ffedb9',
						image: undefined,
						imageOffsetX: 0,
						imageOffsetY: 0,
						position: 'front',
						dropShadow: {
							enabled: false,
							top: 3,
							left: 0,
							blur: 4,
							color: 'rgba(0, 169, 255, 0.85)',
							opacity: 0.65
						}
					},
					track: {
						// background: '#f8f9fa',
						// strokeWidth: '67%',
						margin: 0, // margin is in pixels
						dropShadow: {
							enabled: false,
							top: -3,
							left: 0,
							blur: 4,
							color: 'rgba(0, 169, 255, 0.85)',
							opacity: 0.65
						}
					},
					dataLabels: {
						showOn: 'always',
						name: {
							offsetY: -8,
							show: true,
							color: '#6c757d',
							fontSize: '15px'
						},
						value: {
							formatter: function (val) {
								return val;
							},
							color: '#000',
							fontSize: '25px',
							show: true,
							offsetY: 10,
						}
					}
				}
			},
			fill: {
				type: 'gradient',
				gradient: {
					shade: 'light',
					type: 'horizontal',
					shadeIntensity: 0.5,
					gradientToColors: ['#ffc107'],
					inverseColors: false,
					opacityFrom: 1,
					opacityTo: 1,
					stops: [0, 100]
				}
			},

			colors: ["#ffc107"],
					//series: [671],
					series: ['' + Year3g],
			stroke: {
				lineCap: 'round',
				//dashArray: 4
			},
			// labels: ['2022'],

			labels: [''+ Year3],
		}

		var chart = new ApexCharts(document.querySelector("#yr3"), options);
		chart.render();
	
	// chart yr3summary -Last 3 year response
	var options = {
		series: [{
			name: 'Requests',
			/*data: [240, 160, 671]*/
			data: [''+Year1g, ''+Year2g, ''+Year3g]
		}],
		chart: {
			foreColor: '#9ba7b2',
			type: 'bar',
			height: 270,
			toolbar: {
				show: false
			},
			zoom: {
				enabled: false
			},
			dropShadow: {
				enabled: true,
				top: 3,
				left: 14,
				blur: 4,
				opacity: 0.12,
				color: '#0d6efd',
			},
			sparkline: {
				enabled: false
			}
		},
		markers: {
			size: 0,
			colors: ["#0d6efd"],
			strokeColors: "#fff",
			strokeWidth: 2,
			hover: {
				size: 7,
			}
		},
		plotOptions: {
			bar: {
				horizontal: false,
				columnWidth: '30%',
				endingShape: 'rounded'
			},
		},
		dataLabels: {
			enabled: true
		},
		stroke: {
			show: true,
			width: 3,
			curve: 'smooth'
		},
		colors: ["#0d6efd"],
		xaxis: {
			categories: [''+Year1, ''+Year2, ''+Year3],
		},
		fill: {
			opacity: 1
		},
		tooltip: {
			theme: 'dark',
			fixed: {
				enabled: false
			},
			x: {
				show: true
			},
			y: {
				formatter: function (val) {
					return " " + val + " "
				}
			},
			marker: {
				show: false
			}
		}
	};
	var chart = new ApexCharts(document.querySelector("#yr3summary"), options);
	chart.render();

	// chart rspyr1
	var options =
	{
		chart: {
			height: 180,
			type: 'radialBar',
			toolbar: {
				show: false
			}
		},
		plotOptions: {
			radialBar: {
				//startAngle: -135,
				//endAngle: 225,
				hollow: {
					margin: 0,
					size: '78%',
					background: '#ceffca',
					image: undefined,
					imageOffsetX: 0,
					imageOffsetY: 0,
					position: 'front',
					dropShadow: {
						enabled: false,
						top: 3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				track: {
					// background: '#f8f9fa',
					//strokeWidth: '67%',
					margin: 0, // margin is in pixels
					dropShadow: {
						enabled: false,
						top: -3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				dataLabels: {
					showOn: 'always',
					name: {
						offsetY: -8,
						show: true,
						color: '#6c757d',
						fontSize: '15px'
					},
					value: {
						formatter: function (val) {
							return val;
						},
						color: '#000',
						fontSize: '25px',
						show: true,
						offsetY: 10,
					}
				}
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				shade: 'light',
				type: 'horizontal',
				shadeIntensity: 0.5,
				gradientToColors: ['#17a00e'],
				inverseColors: false,
				opacityFrom: 1,
				opacityTo: 1,
				stops: [0, 100]
			}
		},
		colors: ["#17a00e"],
		//series: [70],
		series: [''+Year1rc],
		stroke: {
			lineCap: 'round',
			width: '5',
			//dashArray: 4
		},
		//labels: ['2020'],
		labels: ['' + Year1],
	}
	var chart = new ApexCharts(document.querySelector("#rspyr1"), options);
	chart.render();
	// chart rspyr2
	var options = {
		chart: {
			height: 180,
			type: 'radialBar',
			toolbar: {
				show: false
			}
		},
		plotOptions: {
			radialBar: {
				//startAngle: -135,
				//endAngle: 225,
				hollow: {
					margin: 0,
					size: '78%',
					background: '#ffd6da',
					image: undefined,
					imageOffsetX: 0,
					imageOffsetY: 0,
					position: 'front',
					dropShadow: {
						enabled: false,
						top: 3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				track: {
					// background: '#f8f9fa',
					//strokeWidth: '67%',
					margin: 0, // margin is in pixels
					dropShadow: {
						enabled: false,
						top: -3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				dataLabels: {
					showOn: 'always',
					name: {
						offsetY: -8,
						show: true,
						color: '#6c757d',
						fontSize: '15px'
					},
					value: {
						formatter: function (val) {
							return val;
						},
						color: '#000',
						fontSize: '25px',
						show: true,
						offsetY: 10,
					}
				}
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				shade: 'light',
				type: 'horizontal',
				shadeIntensity: 0.5,
				gradientToColors: ['#f41127'],
				inverseColors: false,
				opacityFrom: 1,
				opacityTo: 1,
				stops: [0, 100]
			}
		},
		colors: ["#f41127"],
		//series: [100],
		series: ['' + Year2rc],
		stroke: {
			lineCap: 'round',
			//dashArray: 4
		},
		//labels: ["2021"],
		labels: ['' + Year2],
	}
	var chart = new ApexCharts(document.querySelector("#rspyr2"), options);
	chart.render();
	// chart rspyr3
	var options = {
		chart: {
			height: 180,
			type: 'radialBar',
			toolbar: {
				show: false
			}
		},
		plotOptions: {
			radialBar: {
				//startAngle: -135,
				//endAngle: 225,
				hollow: {
					margin: 0,
					size: '78%',
					background: '#ffedb9',
					image: undefined,
					imageOffsetX: 0,
					imageOffsetY: 0,
					position: 'front',
					dropShadow: {
						enabled: false,
						top: 3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				track: {
					// background: '#f8f9fa',
					// strokeWidth: '67%',
					margin: 0, // margin is in pixels
					dropShadow: {
						enabled: false,
						top: -3,
						left: 0,
						blur: 4,
						color: 'rgba(0, 169, 255, 0.85)',
						opacity: 0.65
					}
				},
				dataLabels: {
					showOn: 'always',
					name: {
						offsetY: -8,
						show: true,
						color: '#6c757d',
						fontSize: '15px'
					},
					value: {
						formatter: function (val) {
							return val;
						},
						color: '#000',
						fontSize: '25px',
						show: true,
						offsetY: 10,
					}
				}
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				shade: 'light',
				type: 'horizontal',
				shadeIntensity: 0.5,
				gradientToColors: ['#ffc107'],
				inverseColors: false,
				opacityFrom: 1,
				opacityTo: 1,
				stops: [0, 100]
			}
		},
		colors: ["#ffc107"],
		//series: [60],
		series: ['' + Year3rc],
		stroke: {
			lineCap: 'round',
			//dashArray: 4
		},
		//labels: ['2022'],
		labels: ['' + Year3],
	}
	var chart = new ApexCharts(document.querySelector("#rspyr3"), options);
		chart.render();
	
	// chart rspyr3summary
	var options = {
		series: [{
			name: 'Requests',
			/*data: [80, 100, 60]*/
			data: [Year1rc, Year2rc, Year3rc]
		}],
		chart: {
			foreColor: '#9ba7b2',
			type: 'bar',
			height: 270,
			toolbar: {
				show: false
			},
			zoom: {
				enabled: false
			},
			dropShadow: {
				enabled: true,
				top: 3,
				left: 14,
				blur: 4,
				opacity: 0.12,
				color: '#0d6efd',
			},
			sparkline: {
				enabled: false
			}
		},
		markers: {
			size: 0,
			colors: ["#0d6efd"],
			strokeColors: "#fff",
			strokeWidth: 2,
			hover: {
				size: 7,
			}
		},
		plotOptions: {
			bar: {
				horizontal: false,
				columnWidth: '30%',
				endingShape: 'rounded'
			},
		},
		dataLabels: {
			enabled: true
		},
		stroke: {
			show: true,
			width: 3,
			curve: 'smooth'
		},
		colors: ["#0d6efd"],
		xaxis: {
			/*categories: ['2020', '2021', '2022'],*/
			categories: ['' + Year1, '' + Year2, '' + Year3],
		},
		fill: {
			opacity: 1
		},
		tooltip: {
			theme: 'dark',
			fixed: {
				enabled: false
			},
			x: {
				show: true
			},
			y: {
				formatter: function (val) {
					return " " + val + " "
				}
			},
			marker: {
				show: false
			}
		}
	};
	var chart = new ApexCharts(document.querySelector("#rspyr3summary"), options);
	chart.render();

	
		$(document).ready(function () {
			// AJAX call
			$.ajax({
				url: '/Index?handler=Last5RequestsData',
				method: 'GET',
				dataType: 'json',
				success: function (data) {
					/*alert(data[0].requestId);*/
					// Assuming data is an array of objects
					// Modify the following lines based on your actual data structure
					var dataTable = $('#Dttbl_Last5Requests').DataTable({
						data: data,
						ordering: false,
						paging: false,
						columns: [
							{ data: 'requestId' },
							{ data: 'requestNumber' },
							{ data: 'receivedOnDate' },
							{ data: 'typeOfReqId' },
							{ data: 'requestFrom' },
							{ data: 'requestTo' },
							{ data: 'subProgrammeId' },
							{ data: 'staffResponsible' }
						]
					});
				},
				error: function () {
					console.error('Error fetching data.');
				}
			});
		});


		$(document).ready(function () {
			// AJAX call
			$.ajax({
				url: '/Index?handler=Last5ResponseData',
				method: 'GET',
				dataType: 'json',
				success: function (data) {
					/*alert(data[0].requestId);*/
					// Assuming data is an array of objects
					// Modify the following lines based on your actual data structure
					var dataTable = $('#Dttbl_Last5Response').DataTable({
						
						data: data,
						ordering: false,
						paging: false,
						columns: [
							{ data: 'requestId' },
							{ data: 'receivedOnDate' },
							{ data: 'typeOfReqId' },
							{ data: 'requestFrom' },
							{ data: 'respondedThroughId' },
							{ data: 'respondedYear' }
							]
					});
				},
				error: function () {
					console.error('Error fetching data.');
				}
			});
		});



});


});

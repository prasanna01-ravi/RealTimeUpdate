import { Component, OnInit } from '@angular/core';
import { ConsumerService } from 'src/app/services/consumer.service';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent implements OnInit {

  public chart: any;
  public isIncrease: boolean = false;
  public percentage: number = 0;

  constructor(private consumerService: ConsumerService) { }

  ngOnInit(): void {
    this.createChart();
    this.consumerService.startConnection();

    this.consumerService.message.subscribe(message => {
      this.isIncrease = message.isIncrease;
      this.percentage = message.percentageChange;
      this.chart.data = this.renderData(message);
      this.chart.update();
    });
  }

  createChart() {
    this.chart = new Chart("MyChart", {
      type: 'line',
      data: { labels: [], datasets: [] },
      options: {
        animations: {
          tension: {
            duration: 400,
            easing: 'easeInBounce',
            to: 0.7,
            loop: false
          }
        },
        aspectRatio: 2.5,
        plugins: {
          legend: {
            display: false
          }
        },
        scales: {
          x: {
            ticks: {
              display: false //this will remove only the label
            }
          },
        }
      }
    });
  }

  private renderData(message: any): any {

    const labels = ['', '', '', ''];
    const data = {
      labels: labels,
      datasets: [{
        label: '',
        data: message.values,
        fill: true,
        backgroundColor: message.isIncrease? '#8ede8e': '#ed6d85',
        borderColor: message.isIncrease? '#008000' : '#ff0000',
        tension: 0.25
      }]
    }

    return data;
  }

}
